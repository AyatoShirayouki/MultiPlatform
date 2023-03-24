SET IDENTITY_INSERT [dbo].[PricingPlans] ON

INSERT [dbo].[PricingPlans] ([Id], [Price], [Title], [Description]) VALUES (1, 19, N'Basic Plan', N'Recomended for small companies or beginer freelancers.')
INSERT [dbo].[PricingPlans] ([Id], [Price], [Title], [Description]) VALUES (2, 49, N'Standard Plan', N'Best value for money, recomeded for medium sized comapanies and freelancers of every level.')
INSERT [dbo].[PricingPlans] ([Id], [Price], [Title], [Description]) VALUES (3, 99, N'Extended Plan', N'Recommended for medium to large companies or experienced freelancers, offers the largest variety of features.')

SET IDENTITY_INSERT [dbo].[PricingPlans] OFF
