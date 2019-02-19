using CoreFrame.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CoreFrame.DataRepository
{
    public interface IBusiness<T> where T : class, new()
    {
        #region 事物提交

        void BeginTransaction();
        bool EndTransaction();

        #endregion

        #region 增加数据

        void Insert(T entity);
        void Insert(List<T> entities);
        void BulkInsert(List<T> entities);

        #endregion

        #region 删除数据

        int DeleteAll();
        int Delete(int key);
        int Delete(List<int> keys);
        int Delete(string key);
        int Delete(List<string> keys);
        int Delete(T entity);
        int Delete(List<T> entities);
        int Delete(Expression<Func<T, bool>> condition);

        #endregion

        #region 更新数据

        void Update(T entity);
        void Update(List<T> entities);
        void UpdateAny(T entity, List<string> properties);
        void UpdateAny(List<T> entities, List<string> properties);

        #endregion

        #region 查询数据

        T GetEntity(params object[] keyValue);
        List<T> GetList();
        IQueryable<T> GetPageList(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> predicate);
        IQueryable<T> GetIQueryable();
        IQueryable<T> GetIQueryableList(Expression<Func<T, bool>> predicate);
        List<T> GetDataList(string condition, string keyword, Pagination pagination);
        List<T> GetDataListByIds(List<string> fields,string key, List<int> ids);
        DataTable GetDataTableWithSql(string sql);
        DataTable GetDataTableWithSql(string sql, List<DbParameter> parameters);
        List<T> GetListBySql(string sqlStr);
        List<T> GetListBySql(string sqlStr, List<DbParameter> param);

        #endregion

        #region 执行Sql语句

        void ExecuteSql(string sql);
        void ExecuteSql(string sql, List<DbParameter> spList);

        #endregion

        #region 异步方法
        #region 查询数据

        Task<T> GetEntityAsync(params object[] keyValue);
        Task<List<T>> GetListAsync();
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate, int count, Expression<Func<T, object>> orderKey, bool asc);
        //Task<IQueryable<T>> GetIQueryableAsync();
        //Task<IQueryable<T>> GetIQueryableListAsync(Expression<Func<T, bool>> predicate);
        //Task<List<T>> GetDataListAsync(string condition, string keyword, Pagination pagination);
        //Task<List<T>> GetDataListByIdsAsync(List<string> fields, string key, List<int> ids);
        //Task<DataTable> GetDataTableWithSqlAsync(string sql);
        //Task<DataTable> GetDataTableWithSqlAsync(string sql, List<DbParameter> parameters);
        //Task<List<T>> GetListBySqlAsync(string sqlStr);
        //Task<List<T>> GetListBySqlAsync(string sqlStr, List<DbParameter> param);

        #endregion
        #endregion
    }
}
