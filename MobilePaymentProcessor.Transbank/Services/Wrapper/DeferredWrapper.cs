using MobilePaymentProcessor.Transbank.Infrastructure;
using MobilePaymentProcessor.Transbank.Services.WSCommerceIntegration;
using Microsoft.Web.Services3.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobilePaymentProcessor.Transbank.Infrastructure.Policies;

namespace MobilePaymentProcessor.Transbank.Services.Wrapper
{
    public class DeferredWrapper : ServiceWrapperBase
    {
        public DeferredWrapper(string issuerCertificateName, string transbankCertificateName)
            : base(issuerCertificateName, transbankCertificateName)
        {

        }

        public WSCommerceIntegrationServiceImplService GetDeferredProxy()
        {
            var proxy = new WSCommerceIntegrationServiceImplService();

            proxy.Url = "https://tbk.orangepeople.cl/WSWebpayTransaction/cxf/WSCommerceIntegrationService";

            Policy policy = new Policy();
            CustomPolicyAssertion customPolicy = new CustomPolicyAssertion(this.IssuerCertificateName, this.TransbankCertificateName);
            policy.Assertions.Add(customPolicy);
            proxy.SetPolicy(policy);
            proxy.Timeout = 60000;
            proxy.UseDefaultCredentials = false;

            return proxy;
        }

        public NullifyOutputModel NullifyTransaction(string authorizationCode, string buyOrder, decimal nullifyAmount, decimal authorizedAmount)
        {
            try
            {
                using (var proxy = GetDeferredProxy())
                {
                    var result = proxy.nullify(new nullificationInput()
                    {
                        authorizationCode = authorizationCode,
                        buyOrder = buyOrder,
                        nullifyAmount = nullifyAmount,
                        authorizedAmount = authorizedAmount,
                        commerceId = long.Parse(IssuerCertificateName)
                    });

                    return new NullifyOutputModel()
                    {
                        AuthorizationCode = result.authorizationCode,
                        AuthorizationDate = result.authorizationDateSpecified ? result.authorizationDate : (DateTime?)null,
                        Balance = result.balanceSpecified ? result.balance : (decimal?)null,
                        NullifiedAmount = result.nullifiedAmountSpecified ? result.nullifiedAmount : (decimal?)null,
                        Token = result.token
                    };
                }
            }catch(Exception ex)
            {
                throw ParseException(ex);
            }
        }

        public CaptureOutputModel CaptureTransaction(string authorizationCode,string buyOrder, decimal captureAmount)
        {
            try
            {
                using (var proxy = GetDeferredProxy())
                {
                    var result = proxy.capture(new captureInput()
                    {
                        authorizationCode = authorizationCode,
                        buyOrder = buyOrder,
                        captureAmount = captureAmount,
                        commerceId = long.Parse(IssuerCertificateName)
                    });

                    return new CaptureOutputModel()
                    {
                        AuthorizationCode = result.authorizationCode,
                        AuthorizationDate = result.authorizationDateSpecified ? result.authorizationDate : (DateTime?)null,
                        CapturedAmount = result.capturedAmountSpecified ? result.capturedAmount : (decimal?)null,
                        Token = result.token
                    };
                }
            }
            catch (Exception ex)
            {
                throw ParseException(ex);
            }
        }

    }
}
