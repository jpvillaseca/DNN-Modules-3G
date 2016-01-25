using MobilePaymentProcessor.Transbank.Infrastructure;
using MobilePaymentProcessor.Transbank.Services;
using MobilePaymentProcessor.Transbank.Services.WSCommerceIntegration;
using MobilePaymentProcessor.Transbank.Services.WSWebpay;
using Microsoft.Web.Services3.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MobilePaymentProcessor.Transbank.Infrastructure.Policies;

namespace MobilePaymentProcessor.Transbank.Services.Wrapper
{
    public class NormalWrapper : ServiceWrapperBase
    {
        public NormalWrapper(string issuerCertificateName, string transbankCertificateName)
            : base(issuerCertificateName, transbankCertificateName)
        {

        }

        public WSWebpayServiceImplService GetNormalProxy()
        {
            try
            {
                var proxy = new WSWebpayServiceImplService();

                proxy.Url = "https://tbk.orangepeople.cl/WSWebpayTransaction/cxf/WSWebpayService";

                Policy policy = new Policy();
                CustomPolicyAssertion customPolicy = new CustomPolicyAssertion(this.IssuerCertificateName, this.TransbankCertificateName);
                policy.Assertions.Add(customPolicy);
                proxy.SetPolicy(policy);
                proxy.Timeout = 60000;
                proxy.UseDefaultCredentials = false;

                return proxy;
            }catch(Exception ex)
            {
                throw ParseException(ex);
            }
        }

        public wsInitTransactionOutput InitTransaction(wsInitTransactionInput input)
        {
            try
            {
                wsInitTransactionOutput result;

                using (var proxy = GetNormalProxy())
                {
                    result = proxy.initTransaction(input);
                }

                return result;
            }catch(Exception ex)
            {
                throw ParseException(ex);
            }
        }

        public TransactionResultModel GetTransactionResult(string token)
        {
            try
            {
                using (var proxy = GetNormalProxy())
                {
                    var result = proxy.getTransactionResult(token);

                    return new TransactionResultModel()
                    {
                        AccountingDate = result.accountingDate,
                        BuyOrder = result.buyOrder,
                        CardExpirationDate = result.cardDetail.cardExpirationDate,
                        CardNumber = result.cardDetail.cardNumber,
                        SessionId = result.sessionId,
                        TransactionDate = result.transactionDateSpecified ? result.transactionDate : (DateTime?)null,
                        UrlRedirection = result.urlRedirection,
                        VCI = result.VCI,
                        DetailOutput = result.detailOutput.Select(x => new TransactionDetailModel()
                        {
                            AuthorizationCodeField = x.authorizationCode,
                            PaymentTypeCodeField = x.paymentTypeCode,
                            ResponseCodeField = x.responseCode
                        }).ToList()
                    };
                }
            }catch(Exception ex)
            {
                throw ParseException(ex);
            }
        }

        public void AcknowledgeTrasaction(string token)
        {
            try
            {
                using (var proxy = GetNormalProxy())
                {
                    proxy.acknowledgeTransaction(token);
                }
            }
            catch (Exception ex)
            {
                throw ParseException(ex);
            }
        }
    }
}
