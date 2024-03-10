using Lorby.Domain.Entities;
using Lorby.Domain.Enums;
using Lorby.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Api.SeedData;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
        var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
        
        if (!await appDbContext.EmailTemplates.AnyAsync())
            await appDbContext.SeedEmailTemplates(webHostEnvironment);

        if (appDbContext.ChangeTracker.HasChanges())
            await appDbContext.SaveChangesAsync();
    }
    
    private static async ValueTask SeedEmailTemplates(this AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
    {
        var emailTemplate = new EmailTemplate
        {
            TemplateType = NotificationTemplateType.EmailVerificationNotification,
            Subject = "Confirm you email address",
            Content = """
                      <!DOCTYPE html>
                      <html lang="en">
                      <head>
                          <meta charset="UTF-8">
                          <meta content="width=device-width, initial-scale=1.0" name="viewport">
                          <title>Email Verification</title>
                          <style>
                              body {
                                  font-family: Arial, sans-serif;
                                  background-color: #f4f4f4;
                                  margin: 0;
                                  padding: 0;
                              }
                              .container {
                                  max-width: 600px;
                                  margin: 20px auto;
                                  background: #fff;
                                  padding: 20px;
                                  border-radius: 8px;
                                  box-shadow: 0 4px 8px rgba(0,0,0,0.05);
                              }
                              .header {
                                  background: #007bff;
                                  color: #ffffff;
                                  padding: 10px;
                                  text-align: center;
                                  border-radius: 8px 8px 0 0;
                              }
                              .content {
                                  padding: 20px;
                                  text-align: center;
                              }
                              .footer {
                                  text-align: center;
                                  padding: 10px;
                                  font-size: 0.8em;
                                  color: #666;
                              }
                              .button {
                                  display: inline-block;
                                  padding: 10px 20px;
                                  margin-top: 20px;
                                  background-color: #28a745;
                                  color: white;
                                  text-decoration: none;
                                  border-radius: 5px;
                              }
                          </style>
                      </head>
                      <body>
                      <div class="container">
                          <div class="header">
                              <h1>Please confirm your email address!</h1>
                          </div>
                          <div class="content">
                              <p>Hi, {{UserName}}</p>
                              <p>Thank you for joining us! Please confirm your email address to unveil all features we offer.</p>
                              <p>Here is you verification code: <strong>{{EmailVerificationCode}}</strong></p>
                          </div>
                          <div class="footer">
                              © 2023 Your Company Name. All rights reserved.
                          </div>
                      </div>
                      </body>
                      </html>

                      """
        };

        await appDbContext.EmailTemplates.AddAsync(emailTemplate);
        await appDbContext.SaveChangesAsync();
    }
}