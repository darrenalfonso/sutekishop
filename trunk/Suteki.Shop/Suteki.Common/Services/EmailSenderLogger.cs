﻿using System;
using System.Net.Mail;
using Castle.Core.Logging;
using Suteki.Common.Extensions;

namespace Suteki.Common.Services
{
    public class EmailSenderLogger : IEmailSender
    {
        readonly private IEmailSender emailSender;
        private readonly ILogger logger;

        public EmailSenderLogger(IEmailSender emailSender, ILogger logger)
        {
            this.emailSender = emailSender;
            this.logger = logger;
        }

        public void Send(string toAddress, string subject, string body)
        {
            Send(new[] { toAddress }, subject, body);
        }

        public void Send(string[] toAddress, string subject, string body)
        {
            try
            {
                emailSender.Send(toAddress, subject, body);
                LogEmail(toAddress, subject, body);
            }
            catch (SmtpException e)
            {
                LogEmailFailure(toAddress, subject, body, e);
            }
        }

        private void LogEmailFailure(string[] address, string subject, string body, SmtpException e)
        {
            string message = "Email Failure: {0}\r\nAddresses: {1}\r\nSubject {2}\r\n".With(
                e.Message,
                address.Join(";"),
                subject);
            logger.Error(message, e);
        }

        private void LogEmail(string[] toAddress, string subject, string body)
        {
            string message = "Email sent to: {0}\r\nSubject: {1}\r\n{2}".With(
                toAddress.Join(";"),
                subject,
                body);
            logger.Info(message);
        }
    }
}
