using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureByDesign;

namespace SecureByDesignTests
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>   (Unit Test Class) SafeDatabaseName tests. </summary>
    ///-------------------------------------------------------------------------------------------------
    [TestClass]
    public class SafeDatabaseNameTests
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   (Unit Test Method) tests valid names. </summary>
        /// <param name="value">    The value. </param>
        ///-------------------------------------------------------------------------------------------------
        [TestMethod]
        [DataRow("RTModels2019")]
        [DataRow("RTModels2019073100000000")]
        [DataRow("RTModels2019073100000000")]

        // the following are exactly 64 characters long
        [DataRow("rtqlQ9Po7jGks67K6dY88ZzEtZfeDWIgeyyDGtrVFbjFqUtgoGj7hXD7xo7ICf0N")]
        [DataRow("RtHEYW8Ho3ufrmBmfBdpd5ceaRsqpCWjqJ2V8zIG71vKPpRS70bDtEbesTWiju1R")]
        [DataRow("rTSQdfQnwUAAdMyxPoD1r9layZEmwteiouSGwQwWBy18N0fZJ1sJ66TFgrT36VBR")]
        [DataRow("rtwTa3FErBOcBFZhc9pkRp4jLjFOFoTfxFGXgChQZYju99GQBjUj21VfpZciwEqB")]
        [DataRow("RTks6hFvtLO6pB4Vv1LuChJbmTzOBB1p11MQPfp3VsNAUUklivJTfXgpw8rfGSwf")]
        public void TestValidNames(string value)
        {
            Assert.IsNotNull(new SafeDatabaseName(value));

            SafeDatabaseName initialCatalog = value;
            Assert.IsTrue(initialCatalog.HasValue);

            Assert.AreEqual(value, initialCatalog.Value);

            string castToString = initialCatalog;
            Assert.AreEqual(value,castToString);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   (Unit Test Method) tests names that should result in null. </summary>
        /// <param name="value">    The value. </param>
        ///-------------------------------------------------------------------------------------------------
        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        public void TestNullNames(string value)
        {
            Assert.IsNotNull(new SafeDatabaseName(value));

            SafeDatabaseName initialCatalog = value;
            Assert.IsFalse(initialCatalog.HasValue);

            string castToString = initialCatalog;
            Assert.IsNull(castToString);
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   (Unit Test Method) tests invalid names to ensure they throw an exception. </summary>
        /// <param name="value">    The value. </param>
        ///-------------------------------------------------------------------------------------------------
        [TestMethod]
        [DataRow("Master")]
        [DataRow("Resource")]
        [DataRow("TempDB")]
        [DataRow("Model")]
        [DataRow("MSDB")]
        [DataRow("Distribution")]
        [DataRow("ReportServer")]
        [DataRow("ReportServerTempDB")]

        // the following are too long (65 characters)
        [DataRow("rtqlQ9Po7jGks67K6dY88ZzEtZfeDWIgeyyDGtrVFbjFqUtgoGj7hXD7xo7ICf0N0")]
        [DataRow("RtHEYW8Ho3ufrmBmfBdpd5ceaRsqpCWjqJ2V8zIG71vKPpRS70bDtEbesTWiju1R1")]
        [DataRow("rTSQdfQnwUAAdMyxPoD1r9layZEmwteiouSGwQwWBy18N0fZJ1sJ66TFgrT36VBR2")]
        [DataRow("rtwTa3FErBOcBFZhc9pkRp4jLjFOFoTfxFGXgChQZYju99GQBjUj21VfpZciwEqB3")]
        [DataRow("RTks6hFvtLO6pB4Vv1LuChJbmTzOBB1p11MQPfp3VsNAUUklivJTfXgpw8rfGSwf4")]
        public void TestInvalidNames(string value)
        {
            // Constructor should fail
            Assert.ThrowsException<ArgumentException>(() => new SafeDatabaseName(value));

            // Implicit cast should fail.
            SafeDatabaseName initialCatalog;
            Assert.ThrowsException<ArgumentException>(() => initialCatalog = value);
        }
    }
}
