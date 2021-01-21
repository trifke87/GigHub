using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GigHub.IntegrationTests
{
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope transactionScope;
        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }

        public void AfterTest(TestDetails testDetails)
        {
            transactionScope.Dispose();
        }

        public void BeforeTest(TestDetails testDetails)
        {
            transactionScope = new TransactionScope();
        }
    }
}
