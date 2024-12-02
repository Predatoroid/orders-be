CREATE TABLE Customers (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE FieldTypes (
    Id SERIAL PRIMARY KEY,
    Description VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ModifiedAt TIMESTAMP NULL
);

CREATE TABLE EntityTypes (
    Id SERIAL PRIMARY KEY,
    Description VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ModifiedAt TIMESTAMP NULL
);

CREATE TABLE CustomerFields (
    Id SERIAL PRIMARY KEY,
    CustomerId INT NOT NULL,
    FieldTypeId INT NOT NULL,
    Description VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ModifiedAt TIMESTAMP NULL,
	FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
	FOREIGN KEY (FieldTypeId) REFERENCES FieldTypes(Id)
);

CREATE TABLE FieldOptions (
    Id SERIAL PRIMARY KEY,
    CustomerFieldId INT REFERENCES CustomerFields(Id),
    OptionValue VARCHAR(255) NOT NULL,
	CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
	ModifiedAt TIMESTAMP NULL
);

CREATE TABLE CustomerFieldValues (
    Id SERIAL PRIMARY KEY,
    CustomerFieldId INT NOT NULL,
    FieldOptionId INT NULL,
    FieldValue VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    ModifiedAt TIMESTAMP NULL,
    FOREIGN KEY (CustomerFieldId) REFERENCES CustomerFields(Id),
    FOREIGN KEY (FieldOptionId) REFERENCES FieldOptions(Id)
);

CREATE TABLE CustomerFieldHistory (
    Id SERIAL PRIMARY KEY,ieldId INT NOT NULL,
	CustomerFieldId INT NOT NULL,
    OldValue VARCHAR(255),
    NewValue VARCHAR(255),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CustomerFieldId) REFERENCES CustomerFields(Id)
);

INSERT INTO FieldTypes(Description) VALUES ('Textbox');
INSERT INTO FieldTypes(Description) VALUES ('Dropdown');

INSERT INTO EntityTypes(Description) VALUES ('CustomerFields');
INSERT INTO EntityTypes(Description) VALUES ('FieldOptions');


