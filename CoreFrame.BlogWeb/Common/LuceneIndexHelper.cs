using CoreFrame.Entity.ArticleManage;
using CoreFrame.Util;
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

namespace CoreFrame.BlogWeb.Common
{

    public class LuceneIndexHelper
    {


        public static bool MakeIndex(List<Article> articleList)
        {
            bool retult = false;
            var indexDir = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "lucene");
            if (System.IO.Directory.Exists(indexDir) == false)
            {
                System.IO.Directory.CreateDirectory(indexDir);
            }

            var VERSION = Lucene.Net.Util.LuceneVersion.LUCENE_48;
            var director = FSDirectory.Open(new DirectoryInfo(indexDir));
            var analyzer = new JieBaAnalyzer(TokenizerMode.Search);
            var indexWriterConfig = new IndexWriterConfig(VERSION, analyzer);
            var indexWriter = new IndexWriter(director, indexWriterConfig);

            try
            {
                //Console.WriteLine($"[{DateTime.Now}] UpdateMerchIndex job begin...");

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

                if (!indexWriter.IsClosed)
                {

                    indexWriter.Dispose();

                }
                retult = true;
                //Console.WriteLine($"[{DateTime.Now}] UpdateMerchIndex job end!");
            }
            catch (Exception ex)
            {
                if (!indexWriter.IsClosed)
                {

                    indexWriter.Dispose();

                }
                LogHelper.WriteLog_LocalTxt(ex.ToString());
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
    /// <summary>
    /// JieBaTokenizer继承Lucene.Net.Analyzer的类,覆写TokenStreamComponents函数
    /// </summary>
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

    /// <summary>
    ///  JieBaTokenizer继承Lucene.Net.Tokenizer的类,Tokenizer 是正真将大串文本分成一系列分词的类，在Tokenizer类中，我们必须要覆写 Reset()函数，IncrementToken（）函数
    /// </summary>
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
