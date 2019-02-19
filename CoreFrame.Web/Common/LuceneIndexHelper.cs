using CoreFrame.Entity.ArticleManage;
using jieba.NET;
using JiebaNet.Segmenter;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.TokenAttributes;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Remotion.Linq.Parsing.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFrame.Web.Common
{
    public class ArticleIndex
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public object CreateTime { get; internal set; }
    }
    public class LuceneIndexHelper
    {

        #region 分词测试
        /// <summary>
        /// 分词测试
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public static string Token(string keyword)
        {
            string ret = "";
            var segmenter = new JiebaSegmenter();
            var segments = segmenter.Cut("我来到北京清华大学", cutAll: true);
            ret = string.Format( "【全模式】：{0}" ,string.Join("/ ", segments));

            segments = segmenter.Cut("我来到北京清华大学");  // 默认为精确模式
            ret = string.Format("【精确模式】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("他来到了网易杭研大厦");  // 默认为精确模式，同时也使用HMM模型
            ret = string.Format("【新词识别】：{0}", string.Join("/ ", segments));

            segments = segmenter.CutForSearch("小明硕士毕业于中国科学院计算所，后在日本京都大学深造"); // 搜索引擎模式
            ret = string.Format("【搜索引擎模式】：{0}", string.Join("/ ", segments));

            segments = segmenter.Cut("结过婚的和尚未结过婚的");
            ret = string.Format("【歧义消除】：{0}", string.Join("/ ", segments));
            return ret;
        }
        #endregion
        public static bool MakeIndex(List<ArticleIndex> articleList)
        {
            bool retult = false;
            try
            {
                //Console.WriteLine($"[{DateTime.Now}] UpdateMerchIndex job begin...");

                var indexDir = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "lucene");
                if (System.IO.Directory.Exists(indexDir) == false)
                {
                    System.IO.Directory.CreateDirectory(indexDir);
                }

                var VERSION = Lucene.Net.Util.LuceneVersion.LUCENE_48;
                var director = FSDirectory.Open(new DirectoryInfo(indexDir));
                var analyzer = new JieBaAnalyzer(TokenizerMode.Search);
                var indexWriterConfig = new IndexWriterConfig(VERSION, analyzer);

                using (var indexWriter = new IndexWriter(director, indexWriterConfig))
                {
                    if (File.Exists(Path.Combine(indexDir, "segments.gen")) == true)
                    {
                        indexWriter.DeleteAll();
                    }
                    var index = 1;
                    var size = 200;

                    var count = articleList.Count();

                    if (count > 0)
                    {
                        while (true)
                        {
                            var rs = articleList.OrderBy(t => t.CreateTime)
                            .Skip((index - 1) * size)
                            .Take(size).ToList();

                            if (rs.Count == 0)
                            {
                                break;
                            }

                            var docList = new List<Document>();

                            foreach (var item in rs)
                            {
                                var articleId = item.Id.ToString();

                                var doc = new Document();
                                var field1 = new StringField("articleId", articleId, Field.Store.YES);
                                var field2 = new TextField("title", item.Title?.ToLower(), Field.Store.YES);
                                var field3 = new TextField("summary", item.Summary?.ToLower(), Field.Store.YES);
                                doc.Add(field1);
                                doc.Add(field2);
                                doc.Add(field3);
                                docList.Add(doc);

                            }

                            if (docList.Count > 0)
                            {
                                indexWriter.AddDocuments(docList);
                            }

                            index = index + 1;
                        }

                    }

                }
                retult = true;
                //Console.WriteLine($"[{DateTime.Now}] UpdateMerchIndex job end!");
            }
            catch (Exception ex)
            {
                loghelper.Error(ex, ex.InnerException.ToString());
                //Console.WriteLine($"UpdateMerchIndex ex={ex}");
            }
            return retult;

        }

        public static List<int> Search(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }
            key = key.Trim().ToLower();

            var rs = new List<int>();

            try
            {
                var indexDir = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "lucene");

                var VERSION = Lucene.Net.Util.LuceneVersion.LUCENE_48;

                if (System.IO.Directory.Exists(indexDir) == true)
                {
                    var reader = DirectoryReader.Open(FSDirectory.Open(new DirectoryInfo(indexDir)));
                    var search = new IndexSearcher(reader);

                    var directory = FSDirectory.Open(new DirectoryInfo(indexDir), NoLockFactory.GetNoLockFactory());
                    var reader2 = DirectoryReader.Open(directory);
                    var search2 = new IndexSearcher(reader2);

                    //var parser = new QueryParser(VERSION, "title", new JieBaAnalyzer(TokenizerMode.Search));
                    var booleanQuery = new BooleanQuery();

                    var list = CutKeyWord(key);
                    foreach (var word in list)
                    {
                        var query1 = new TermQuery(new Term("title", word));
                        var query2 = new TermQuery(new Term("summary", word));
                        booleanQuery.Add(query1, Occur.SHOULD);
                        booleanQuery.Add(query2, Occur.SHOULD);
                    }

                    var collector = TopScoreDocCollector.Create(1000, true);
                    search2.Search(booleanQuery, null, collector);
                    var docs = collector.GetTopDocs(0, collector.TotalHits).ScoreDocs;

                    foreach (var d in docs)
                    {
                        var num = d.Doc;
                        var document = search.Doc(num);// 拿到指定的文档

                        var articleId = document.Get("articleId");
                        //var name = document.Get("title");

                        if (int.TryParse(articleId, out int mid) == true)
                        {
                            rs.Add(mid);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SearchMerchs ex={ex}");
            }

            return rs;
        }

        protected static List<string> CutKeyWord(string key)
        {
            var rs = new List<string>();
            var segmenter = new JiebaSegmenter();
            var list = segmenter.Cut(key);
            if (list != null && list.Count() > 0)
            {
                foreach (var item in list)
                {
                    if (string.IsNullOrEmpty(item) || item.Length <= 1)
                    {
                        continue;
                    }

                    rs.Add(item);
                }
            }
            return rs;
        }
    }

    public class JieBaAnalyzer : Analyzer
    {
        public TokenizerMode mode;
        public JieBaAnalyzer(TokenizerMode Mode)
            : base()
        {
            this.mode = Mode;
        }

        protected override TokenStreamComponents CreateComponents(string filedName, TextReader reader)
        {
            var tokenizer = new JieBaTokenizer(reader, mode);

            var tokenstream = (TokenStream)new LowerCaseFilter(Lucene.Net.Util.LuceneVersion.LUCENE_48, tokenizer);

            tokenstream.AddAttribute<ICharTermAttribute>();
            tokenstream.AddAttribute<IOffsetAttribute>();

            return new TokenStreamComponents(tokenizer, tokenstream);
        }
    }


    public class JieBaTokenizer
        : Tokenizer
    {
        private static object _LockObj = new object();
        private static bool _Inited = false;
        private System.Collections.Generic.List<JiebaNet.Segmenter.Token> _WordList = new List<JiebaNet.Segmenter.Token>();
        private string _InputText;
        private bool _OriginalResult = false;

        private ICharTermAttribute termAtt;
        private IOffsetAttribute offsetAtt;
        private IPositionIncrementAttribute posIncrAtt;
        private ITypeAttribute typeAtt;

        private List<string> stopWords = new List<string>();
        private string stopUrl = "./stopwords.txt";
        private JiebaSegmenter segmenter;

        private System.Collections.Generic.IEnumerator<JiebaNet.Segmenter.Token> iter;
        private int start = 0;

        private TokenizerMode mode;



        public JieBaTokenizer(TextReader input, TokenizerMode Mode)
            : base(AttributeFactory.DEFAULT_ATTRIBUTE_FACTORY, input)
        {
            segmenter = new JiebaSegmenter();
            mode = Mode;
            StreamReader rd = File.OpenText(stopUrl);
            string s = "";
            while ((s = rd.ReadLine()) != null)
            {
                stopWords.Add(s);
            }

            Init();

        }

        private void Init()
        {
            termAtt = AddAttribute<ICharTermAttribute>();
            offsetAtt = AddAttribute<IOffsetAttribute>();
            posIncrAtt = AddAttribute<IPositionIncrementAttribute>();
            typeAtt = AddAttribute<ITypeAttribute>();
        }

        private string ReadToEnd(TextReader input)
        {
            return input.ReadToEnd();
        }

        public sealed override Boolean IncrementToken()
        {
            ClearAttributes();

            Lucene.Net.Analysis.Token word = Next();
            if (word != null)
            {
                var buffer = word.ToString();
                termAtt.SetEmpty().Append(buffer);
                offsetAtt.SetOffset(CorrectOffset(word.StartOffset), CorrectOffset(word.EndOffset));
                typeAtt.Type = word.Type;
                return true;
            }
            End();
            this.Dispose();
            return false;

        }

        public Lucene.Net.Analysis.Token Next()
        {

            int length = 0;
            bool res = iter.MoveNext();
            Lucene.Net.Analysis.Token token;
            if (res)
            {
                JiebaNet.Segmenter.Token word = iter.Current;

                token = new Lucene.Net.Analysis.Token(word.Word, word.StartIndex, word.EndIndex);
                // Console.WriteLine("xxxxxxxxxxxxxxxx分词："+word.Word+"xxxxxxxxxxx起始位置："+word.StartIndex+"xxxxxxxxxx结束位置"+word.EndIndex);
                start += length;
                return token;

            }
            else
                return null;

        }

        public override void Reset()
        {
            base.Reset();

            _InputText = ReadToEnd(base.m_input);
            RemoveStopWords(segmenter.Tokenize(_InputText, mode));


            start = 0;
            iter = _WordList.GetEnumerator();

        }

        public void RemoveStopWords(System.Collections.Generic.IEnumerable<JiebaNet.Segmenter.Token> words)
        {
            _WordList.Clear();

            foreach (var x in words)
            {
                if (stopWords.IndexOf(x.Word) == -1)
                {
                    _WordList.Add(x);
                }
            }

        }

    }
}
