USE [ForeignShare]
GO
/****** Object:  Table [dbo].[TPlusOne]    Script Date: 2018-9-5 16:16:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TPlusOne](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Product] [nvarchar](50) NOT NULL,
	[Exchange] [nvarchar](50) NOT NULL,
	[TPlusOneStartTime] [time](7) NOT NULL,
	[TPlusOneEndTime] [time](7) NOT NULL,
 CONSTRAINT [PK_TPlusOne] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[TPlusOne] ON 

INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (1, N'FEM', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (4, N'FEM', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070084B1109B0000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (7, N'FEQ', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (8, N'FEQ', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070084B1109B0000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (9, N'LRA', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (10, N'LRC', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (13, N'LRZ', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (14, N'LRN', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (15, N'LRS', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (17, N'LRR', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (19, N'GDR', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (20, N'GDU', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (21, N'JAU', N'TOCOM', CAST(0x07004C64EB810000 AS Time), CAST(0x0700D4F3B7250000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (22, N'JPL', N'TOCOM', CAST(0x07004C64EB810000 AS Time), CAST(0x0700D4F3B7250000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (23, N'JCO', N'TOCOM', CAST(0x07004C64EB810000 AS Time), CAST(0x0700D4F3B7250000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (24, N'JB', N'SGX', CAST(0x07001CEDAE920000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (25, N'JRU', N'TOCOM', CAST(0x07004C64EB810000 AS Time), CAST(0x070050CFDF960000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (26, N'PF', N'APEX', CAST(0x070024C397BC0000 AS Time), CAST(0x07008C87F9C40000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (27, N'FCE', N'LiffeEurone', CAST(0x070034E230040000 AS Time), CAST(0x0700A01187210000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (28, N'TW', N'SGXQ', CAST(0x0700CA2E71770000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (29, N'NK', N'SGXQ', CAST(0x0700BAB1077D0000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (30, N'NS', N'SGXQ', CAST(0x0700BAB1077D0000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (31, N'CN', N'SGXQ', CAST(0x0700E80A7E8E0000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (32, N'NCH', N'SGXQ', CAST(0x0700B893419F0000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (33, N'CH', N'SGXQ', CAST(0x0700027C96900000 AS Time), CAST(0x0700D088C3100000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (34, N'IN', N'SGXQ', CAST(0x07004052769C0000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (35, N'FEF', N'SGXQ', CAST(0x07003AC9BBA90000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (36, N'UC', N'SGXQ', CAST(0x07006A40F8980000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (37, N'CY', N'SGXQ', CAST(0x07009E22299D0000 AS Time), CAST(0x0700D088C3100000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (38, N'SGP', N'SGXQ', CAST(0x0700D88D14940000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (39, N'MY', N'SGXQ', CAST(0x0700F2FE2C960000 AS Time), CAST(0x0700EE64D0270000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (40, N'HHI', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (41, N'HSI', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (42, N'MHI', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (43, N'MCH', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (50, N'CUS', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (51, N'CEU', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (52, N'CJP', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (53, N'CAU', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (54, N'UCN', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (55, N'MXJ', N'HKEX', CAST(0x0700027C96900000 AS Time), CAST(0x070068C461080000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (56, N'IB', N'ASX', CAST(0x0700846B4D770000 AS Time), CAST(0x0700A01187210000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (57, N'IR', N'ASX', CAST(0x0700E0D776760000 AS Time), CAST(0x0700A01187210000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (58, N'IR', N'ASX', CAST(0x0700489CD87E0000 AS Time), CAST(0x070008D6E8290000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (59, N'IR', N'ASX', CAST(0x0700E0D776760000 AS Time), CAST(0x0700B4284D8A0000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (60, N'YT', N'ASX', CAST(0x07006C5EBE760000 AS Time), CAST(0x0700A01187210000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (61, N'YT', N'ASX', CAST(0x0700D422207F0000 AS Time), CAST(0x070008D6E8290000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (62, N'YT', N'ASX', CAST(0x07006C5EBE760000 AS Time), CAST(0x0700D4F3B7250000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (63, N'XT', N'ASX', CAST(0x0700F8E405770000 AS Time), CAST(0x0700A01187210000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (64, N'XT', N'ASX', CAST(0x070060A9677F0000 AS Time), CAST(0x070008D6E8290000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (65, N'XT', N'ASX', CAST(0x0700F8E405770000 AS Time), CAST(0x0700D4F3B7250000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (66, N'XX', N'ASX', CAST(0x0700F8E405770000 AS Time), CAST(0x0700A01187210000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (67, N'TOPIX', N'JPX', CAST(0x07004C64EB810000 AS Time), CAST(0x0700D4F3B7250000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (68, N'Mini NK', N'JPX(OSE)', CAST(0x07004C64EB810000 AS Time), CAST(0x0700D4F3B7250000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (69, N'NK225', N'JPX', CAST(0x07004C64EB810000 AS Time), CAST(0x0700D4F3B7250000 AS Time))
INSERT [dbo].[TPlusOne] ([ID], [Product], [Exchange], [TPlusOneStartTime], [TPlusOneEndTime]) VALUES (70, N'FTI', N'Euronext', CAST(0x07009CA6920C0000 AS Time), CAST(0x070008D6E8290000 AS Time))
SET IDENTITY_INSERT [dbo].[TPlusOne] OFF
