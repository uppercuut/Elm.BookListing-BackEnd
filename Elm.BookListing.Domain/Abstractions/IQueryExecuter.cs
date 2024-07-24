using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Elm.BookListing.Domain.Abstractions
{
    public interface IQueryExecuter
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null);
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null);
        Task<IEnumerable<object>> QueryAsync(Type returnType, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null);
        Task<object> QueryFirstOrDefaultAsync(Type returnType, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null);
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null, params object[] queryStrings);
        Task QueryMultipleAsync(string sql, Action<GridReader> mapResultSets, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null);
        Task QueryMultiple(string sql, Action<GridReader> mapResultSets, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null);
    }
}
