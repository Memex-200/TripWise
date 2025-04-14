 -- Insert Countries
INSERT INTO Country (CountryCode, CountryName) VALUES
('USA', 'United States'),
('GBR', 'United Kingdom'),
('JPN', 'Japan'),
('FRA', 'France'),
('GER', 'Germany');

-- Insert Company Types
INSERT INTO CompanyTypes (TypeName) VALUES
('Airline'),
('Railway'),
('Bus Company'),
('Cruise Line'),
('Car Rental');

-- Insert Room Types
INSERT INTO RoomTypes (TypeName) VALUES
('Single'),
('Double'),
('Suite'),
('Deluxe'),
('Family Room');

-- Insert Ticket Types
INSERT INTO TicketTypes (TypeName) VALUES
('Economy'),
('Business'),
('First Class'),
('Sleeper'),
('VIP');

-- Insert Agents
INSERT INTO Agents (FirstName, LastName, Active) VALUES
('John', 'Doe', 1),
('Jane', 'Smith', 1),
('Bob', 'Johnson', 0),
('Alice', 'Brown', 1);

-- Insert Cities
INSERT INTO City (CityName, CountryCode) VALUES
('New York', 'USA'),
('Los Angeles', 'USA'),
('London', 'GBR'),
('Tokyo', 'JPN'),
('Paris', 'FRA'),
('Berlin', 'GER');

-- Insert Users (Simplified for example - passwords should be properly hashed in real scenarios)
INSERT INTO AspNetUsers (
    FirstName, LastName, Address, Email, Details, Phone, Mobile, CustomerFrom,
    UserName, NormalizedUserName, EmailConfirmed, PasswordHash, SecurityStamp,
    ConcurrencyStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount
) VALUES (
    'Mike', 'Jones', '123 Main St', 'mike@example.com', 'Regular customer',
    '+123456789', '+987654321', GETDATE(),
    'mike@example.com', 'MIKE@EXAMPLE.COM', 1,
    'AQAAAAIAAYagAAAAENhwkL85k58XW0Z21dRmQ4mGUVG9Jz7TwbE5LR5L1vRfF7JYKB0DpGxD6Pf5zXWt1Q==',
    '7D5A9F4E-3A7C-48', '7a6c7f3d-4e2b-49', 0, 0, 0, 0
);

-- Insert Promo Offers
INSERT INTO PromoOffers (PromoOfferName, ActiveFrom, ActiveTo, AgentCode) VALUES
('Summer Sale', '2024-06-01', '2024-08-31', 1),
('Winter Special', '2024-12-01', '2025-02-28', 2),
('Golden Week', '2024-04-25', '2024-05-06', 4);

-- Insert Hotels
INSERT INTO Hotels (HotelName, HotelAddress, Details, IsPartner, Active, Image, CityId) VALUES
('Grand Hotel', '123 Main St', 'Luxury 5-star hotel', 1, 1, 'grand.jpg', 1),
('Tokyo Tower Inn', '1 Chome-1-1 Shibakoen', 'City center location', 1, 1, 'tokyo.jpg', 4),
('Paris Paradise', '42 Rue de Rivoli', 'Boutique hotel near Louvre', 1, 1, 'paris.jpg', 5);

-- Insert Transport Companies
INSERT INTO TransportCompanies (CompanyName, HQAddress, Description, IsPartner, Active, Image, CityId, CompanyTypeId) VALUES
('Global Airlines', '789 Airport Rd', 'International carrier', 1, 1, 'global.jpg', 1, 1),
('EuroRail Express', '101 Station St', 'High-speed trains', 1, 1, 'eurorail.jpg', 3, 2),
('Tokyo Metro', '2 Chome-1-1 Marunouchi', 'Metropolitan transport', 1, 1, 'metro.jpg', 4, 3);

-- Insert Hotel Services
INSERT INTO Hotel_Service (HotelId, RoomTypeId, ServicePrice, Active) VALUES
(1, 3, 450.00, 1),
(1, 2, 300.00, 1),
(2, 1, 150.00, 1),
(3, 4, 600.00, 1),
(3, 5, 800.00, 1);

-- Insert Transport Services
INSERT INTO Transport_Service (TransportCompanyId, TicketTypeId, FromCityId, ToCityId, ServicePrice, Active) VALUES
(1, 2, 1, 4, 1200.00, 1),
(2, 3, 3, 5, 450.00, 1),
(3, 1, 4, 4, 5.00, 1),  -- Local metro service
(1, 3, 5, 1, 1500.00, 1);

-- Insert Offers
INSERT INTO Offers (OfferName, TimeCreated, ActiveFrom, ActiveTo, TimeAccepted, Accepted, Image, PromoOfferId, AgentId, CustomerId) VALUES
('NYC-Tokyo Package', GETDATE(), '2024-05-01', '2024-12-31', NULL, 0, 'package1.jpg', 1, 1, 1),
('European Tour', GETDATE(), '2024-06-01', '2024-11-30', GETDATE(), 1, 'euro.jpg', NULL, 2, 1),
('Tokyo Metro Special', GETDATE(), '2024-04-20', '2024-12-31', NULL, 0, 'metro.jpg', 3, 4, 1);

-- Insert Contracts
INSERT INTO Contracts (CustomerId, AgentId, OfferCode, TimeSigned, TotalPrice, PaymentDate, PaymentTime, PaymentAmount, Refunded) VALUES
(1, 1, 2, GETDATE(), 2500.00, GETDATE(), CONVERT(time, GETDATE()), 2500.00, 0);

-- Insert Offer Hotel Services
INSERT INTO OfferHotelServices (HotelServiceId, Price, DiscountPercent, FinalServicePrice, Description, OfferCode1) VALUES
(1, 400.00, 10, 360.00, 'Suite Discount', 1),
(3, 140.00, 5, 133.00, 'Early Bird Special', 2);

-- Insert Offer Transport Services
INSERT INTO OfferTransportServices (TransportServiceId, Price, DiscountPercent, FinalServicePrice, Description, OfferCode1) VALUES
(1, 1100.00, 8, 1012.00, 'Business Class Upgrade', 1),
(2, 400.00, 10, 360.00, 'First Class Special', 2);

-- Insert Promo Offer Hotel Services
INSERT INTO PromoOfferHotelServices (PromoOfferId, HotelServiceId, Price, DiscountPercent, FinalServicePrice, Description) VALUES
(1, 2, 280.00, 15, 238.00, 'Summer Double Room Special'),
(3, 4, 550.00, 8, 506.00, 'Golden Week Deluxe');

-- Insert Promo Offer Transport Services
INSERT INTO PromoOfferTransportServices (PromoOfferId, TransportServiceId, Price, DiscountPercent, FinalServicePrice, Description) VALUES
(2, 4, 1400.00, 7, 1302.00, 'Winter First Class'),
(3, 3, 4.50, 10, 4.05, 'Metro Golden Week Discount');

-- Insert sample Offers
INSERT INTO Offers (
    OfferName, 
    TimeCreated, 
    ActiveFrom, 
    ActiveTo, 
    TimeAccepted, 
    Accepted, 
    Image, 
    PromoOfferId, 
    AgentId, 
    CustomerId
) VALUES
(
    'Summer Vacation Package', 
    GETDATE(), 
    '2024-06-15', 
    '2024-08-31', 
    NULL, 
    0, 
    'summer-package.jpg', 
    1,  -- Reference to PromoOffers.PromoOfferCode
    1,  -- Reference to Agents.AgentCode
    1   -- Reference to AspNetUsers.Id
),
(
    'Business Class Europe Tour', 
    GETDATE(), 
    '2024-07-01', 
    '2024-09-30', 
    '2024-05-15', 
    1, 
    'europe-business.jpg', 
    NULL, 
    2, 
    1
),
(
    'Tokyo Golden Week Special', 
    GETDATE(), 
    '2024-04-25', 
    '2024-05-06', 
    NULL, 
    0, 
    'tokyo-golden.jpg', 
    3, 
    4, 
    1
),
(
    'Winter Ski Resort Package', 
    GETDATE(), 
    '2024-12-10', 
    '2025-01-15', 
    NULL, 
    0, 
    'ski-package.jpg', 
    2, 
    1, 
    1
),
(
    'Last Minute City Break', 
    GETDATE(), 
    GETDATE(),  -- Active immediately
    DATEADD(week, 2, GETDATE()),  -- Active for 2 weeks
    NULL, 
    0, 
    'city-break.jpg', 
    NULL, 
    3, 
    1
);

-- Continue with existing migrations
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250411205108_AddImageColumns', N'9.0.2');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250411205628_AddImageColumns_', N'9.0.2');



COMMIT;
GO