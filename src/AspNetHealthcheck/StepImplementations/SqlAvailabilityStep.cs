using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AspNetHealthCheck.Logging;

namespace AspNetHealthCheck.StepImplementations
{
    public class SqlAvailabilityStep : HealthCheckStep
    {
        private readonly string _databaseName;
        private readonly string _connectionString;
        private readonly bool _isCritical;
        private static readonly ILog Log = LogProvider.GetLogger(typeof(SqlAvailabilityStep));

        public SqlAvailabilityStep(
            string databaseName,
            string connectionString,
            bool isCritical)
        {
            _databaseName = databaseName;
            _connectionString = connectionString;
            _isCritical = isCritical;
        }

        public override string StepName
        {
            get { return string.Format("Database {0} availability", _databaseName); }
        }

        public override string StepDescription
        {
            get { return string.Format("Checking for the availability of database '{0}'", _databaseName); }
        }

        public override bool IsCritical
        {
            get { return _isCritical; }
        }

        protected override Tuple<bool, IEnumerable<HealthCheckStepResponse>> CheckIsHealthy()
        {
            bool result;

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {

                    connection.Open();
                    var command = new SqlCommand("SELECT 1;", connection);
                    command.ExecuteNonQuery();
                }
                result = true;
            }
            catch (Exception e)
            {
                Log.ErrorException("Error checking SQL Availability", e);
                result = false;
            }

            return new Tuple<bool, IEnumerable<HealthCheckStepResponse>>(result, new HealthCheckStepResponse[0]);

        }
    }
}
