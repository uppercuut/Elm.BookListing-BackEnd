using Dapper;
using Elm.BookListing.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Elm.BookListing.Application.Queries
{
    public class QueryExecuter : IQueryExecuter
    {
        private readonly QueryExecuterOptions _options;

        public QueryExecuter(QueryExecuterOptions options)
        {
            _options = options;
        }

        public Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            return ExecuteQueryAsync(sql, connection => connection.QueryAsync<T>(sql, param, null, commandTimeout, commandType), param, connectionString);
        }
        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            return ExecuteQueryAsync(sql, connection => connection.QueryAsync<TFirst, TSecond,  TReturn>(sql, map), param, connectionString);
        }
        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            return ExecuteQueryAsync(sql, connection => connection.QueryAsync<TFirst, TSecond, TThird, TReturn>(sql, map), param, connectionString);
        }
        public Task<IEnumerable<TReturn>> QueryAsync<TFirst,TSecond,TThird,TFourth,TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            return ExecuteQueryAsync(sql, connection => connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(sql, map), param, connectionString);
        }
        public Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            return ExecuteQueryAsync(sql, connection => connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql, map), param, connectionString);
        }
        public Task<IEnumerable<object>> QueryAsync(Type returnType, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            return ExecuteQueryAsync(sql, connection => connection.QueryAsync(returnType, sql, param, null, commandTimeout, commandType), param, connectionString);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            return ExecuteQueryAsync(sql, connection => connection.QueryFirstOrDefaultAsync<T>(sql, param, null, commandTimeout, commandType), param, connectionString);
        }

        public Task<object> QueryFirstOrDefaultAsync(Type returnType, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            return ExecuteQueryAsync(sql, connection => connection.QueryFirstOrDefaultAsync(returnType, sql, param, null, commandTimeout, commandType), param, connectionString);
        }

        public Task<T> ExecuteScalarAsync<T>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null, params object[] queryStrings)
        {
            return ExecuteQueryAsync(sql, connection => connection.ExecuteScalarAsync<T>(sql, param, null, commandTimeout, commandType), param, connectionString);
        }

        public async Task QueryMultipleAsync(string sql, Action<SqlMapper.GridReader> mapResultSets, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            await ExecuteQueryAsync(sql, async connection =>
            {
                var results = await connection.QueryMultipleAsync(sql, param, null, commandTimeout, commandType);
                mapResultSets(results);
                return results;
            }, param, connectionString: connectionString);
        }
        public async Task QueryMultiple(string sql, Action<SqlMapper.GridReader> mapResultSets, object param = null, int? commandTimeout = null, CommandType? commandType = null, string connectionString = null)
        {
            await ExecuteQueryAsync(sql, async connection =>
            {
                var results =   connection.QueryMultiple(sql, param, null, commandTimeout, commandType);
                mapResultSets(results);
                return results;
            }, param, connectionString: connectionString);
        }

        private async Task<T> ExecuteQueryAsync<T>(string sql, Func<IDbConnection, Task<T>> executerFunc, object param, string connectionString = null, params object[] queryStrings)
        {
            var stopwatch = Stopwatch.StartNew();
            using (var connection = new SqlConnection(connectionString ?? _options.ConnectionString))
            {
                try
                {
                    return await executerFunc(connection);
                }
                finally
                {
                    stopwatch.Stop();
                    //todo..log execution time
                }
            }
        }
    }
}
