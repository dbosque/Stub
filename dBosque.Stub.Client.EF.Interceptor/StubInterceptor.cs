using dBosque.Stub.Client.Socket.Exception;
using dBosque.Stub.Client.Socket.Process;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;

namespace dBosque.Stub.Client.EF.Interceptor
{

    /// <summary>
    /// Actual interceptor class
    /// </summary>
    internal sealed class StubInterceptor : IDbCommandInterceptor
    {
        private readonly StubProxy _proxy;
        private readonly bool _passthroughOnNotFound;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="passthroughOnNotFound"></param>
        /// <param name="port"></param>
        /// <param name="server"></param>
        internal StubInterceptor(string server, int port, bool passthroughOnNotFound)
        {
            _proxy = StubProxy. (server, port);
            _passthroughOnNotFound = passthroughOnNotFound;
        }

        /// <summary>
        /// Handle the generic stubresult
        /// </summary>
        /// <typeparam name="T">The type of data to return</typeparam>
        /// <param name="interceptionContext">The interception context</param>
        /// <param name="result">The actual result from the stub</param>
        private void HandleStubResult<T>(DbCommandInterceptionContext<T> interceptionContext, StubProxyResult<T> result)
        {
            if (result.Success)
                interceptionContext.Result = result.Data;
            // Special case when passthrough is enabled, do not return an error, ust trace the error
            else if (_passthroughOnNotFound && result.ErrorCode == 404)
                System.Diagnostics.Trace.TraceInformation(result.ErrorMessage);            
            else
                interceptionContext.Exception = new DataNotFoundException(result.ErrorMessage);
        
        }

        /// <summary>
        /// Dispatch the command to the proxy
        /// </summary>
        /// <typeparam name="T">The type of data to return</typeparam>
        /// <param name="command">The command to execute</param>
        /// <param name="interceptionContext">The interception context</param>
        /// <param name="executeAction">The action to call to process the command</param>
        private void Dispatch<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext, System.Func<DbCommand, StubProxyResult<T>> executeAction)
        {
            try
            {
                var result = executeAction(command);
                HandleStubResult(interceptionContext, result);
            }
            catch (StubNotAvailableException ex)
            {
                interceptionContext.Exception = new StubNotAvailableException(ex);
            }
            catch (System.Exception ex)
            {
                interceptionContext.Exception = ex;
            }

        }

        ///<summary>
        ///This method is called after a call to <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" />  or
        ///one of its async counterparts is made. The result used by Entity Framework can be changed by setting
        ///<see cref="P:System.Data.Entity.Infrastructure.Interception.DbCommandInterceptionContext`1.Result" />.
        ///</summary>
        ///<remarks>
        ///For async operations this method is not called until after the async task has completed
        ///or failed.
        ///</remarks>
        ///<param name="command">The command being executed.</param>
        ///<param name="interceptionContext">Contextual information associated with the call.</param>
        void IDbCommandInterceptor.NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
           // nothing
        }

        ///<summary>
        ///This method is called before a call to <see cref="M:System.Data.Common.DbCommand.ExecuteNonQuery" /> or
        ///one of its async counterparts is made.
        ///</summary>
        ///<param name="command">The command being executed.</param>
        ///<param name="interceptionContext">Contextual information associated with the call.</param>
        void IDbCommandInterceptor.NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Dispatch(command, interceptionContext, _proxy.Execute<int>);         
        }

        ///<summary>
        ///This method is called after a call to <see cref="M:System.Data.Common.DbCommand.ExecuteReader(System.Data.CommandBehavior)" /> or
        ///one of its async counterparts is made. The result used by Entity Framework can be changed by setting
        ///<see cref="P:System.Data.Entity.Infrastructure.Interception.DbCommandInterceptionContext`1.Result" />.
        ///</summary>
        ///<remarks>
        ///For async operations this method is not called until after the async task has completed
        ///or failed.
        ///</remarks>
        ///<param name="command">The command being executed.</param>
        ///<param name="interceptionContext">Contextual information associated with the call.</param>
        void IDbCommandInterceptor.ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            // nothing
        }

        ///<summary>
        ///This method is called before a call to <see cref="M:System.Data.Common.DbCommand.ExecuteReader(System.Data.CommandBehavior)" /> or
        ///one of its async counterparts is made.
        ///</summary>
        ///<param name="command">The command being executed.</param>
        ///<param name="interceptionContext">Contextual information associated with the call.</param>
        void IDbCommandInterceptor.ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Dispatch(command, interceptionContext, _proxy.ExecuteReader);            
        }

        ///<summary>
        ///This method is called after a call to <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" /> or
        ///one of its async counterparts is made. The result used by Entity Framework can be changed by setting
        ///<see cref="P:System.Data.Entity.Infrastructure.Interception.DbCommandInterceptionContext`1.Result" />.
        ///</summary>
        ///<remarks>
        ///For async operations this method is not called until after the async task has completed
        ///or failed.
        ///</remarks>
        ///<param name="command">The command being executed.</param>
        ///<param name="interceptionContext">Contextual information associated with the call.</param>
        void IDbCommandInterceptor.ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            // nothing
        }

        ///<summary>
        ///This method is called before a call to <see cref="M:System.Data.Common.DbCommand.ExecuteScalar" /> or
        ///one of its async counterparts is made.
        ///</summary>
        ///<param name="command">The command being executed.</param>
        ///<param name="interceptionContext">Contextual information associated with the call.</param>
        void IDbCommandInterceptor.ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Dispatch(command, interceptionContext, _proxy.ExecuteClass<object>);            
        }      
    }

   



}