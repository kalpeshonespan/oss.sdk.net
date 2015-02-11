using System;
using NUnit.Framework;
using Silanis.ESL.SDK;
using NUnit.Framework.SyntaxHelpers;
using System.Collections.Generic;
using TrotiNet;

namespace SDK.Examples
{
    public class ProxyConfigurationExampleTest
    {
        [Test]
        public void verifyResult()
        {
            ExecutionByHttpProxy();
            //ExecutionByHttpProxyWithCredentials();
        }

        public void ExecutionByHttpProxy(){

            ProxyConfigurationExample example = new ProxyConfigurationExample(Props.GetInstance());
            example.ExecuteViaHttpProxy();
            DocumentPackage documentPackage1 = example.EslClientWithHttpProxy.GetPackage(example.PackageId);
            Assert.AreEqual(documentPackage1.Id.Id, example.PackageId.Id);

        }

        public void ExecutionByHttpProxyWithCredentials(){

            ProxyConfigurationExample example = new ProxyConfigurationExample(Props.GetInstance());
            example.ExecuteViaHttpProxyWithCredentials();
            DocumentPackage documentPackage2 = example.EslClientWithHttpProxyHasCredentials.GetPackage(example.PackageId);
            Assert.AreEqual(documentPackage2.Id.Id, example.PackageId.Id);

        }

    }
}

