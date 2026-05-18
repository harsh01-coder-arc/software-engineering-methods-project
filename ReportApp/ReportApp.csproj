CREATE TABLE Countries (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Population BIGINT NOT NULL
);

CREATE TABLE Cities (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    CountryId INT NOT NULL,
    Name VARCHAR(100) NOT NULL,
    Population BIGINT NOT NULL,
    FOREIGN KEY (CountryId) REFERENCES Countries(Id)
);

INSERT INTO Countries (Name, Population) VALUES
('India', 1428000000),
('China', 1412000000),
('USA', 339000000);

INSERT INTO Cities (CountryId, Name, Population) VALUES
(1, 'Delhi', 33000000),
(1, 'Mumbai', 20400000),
(2, 'Shanghai', 24800000),
(3, 'New York', 8400000);
