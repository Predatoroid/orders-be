-- Δημιουργία πίνακα για τα πεδία των πελατών (Custom Fields)
CREATE TABLE CustomerCustomFields (
    Id INT PRIMARY KEY IDENTITY,
    CustomerId INT NOT NULL,
    FieldName NVARCHAR(255) NOT NULL,
    FieldType NVARCHAR(50) NOT NULL, -- 'textbox' ή 'dropdown'
    CreatedAt DATETIME DEFAULT GETDATE(),
    ModifiedAt DATETIME
);

-- Δημιουργία πίνακα για τις επιλογές dropdown
CREATE TABLE CustomFieldOptions (
    Id INT PRIMARY KEY IDENTITY,
    CustomFieldId INT FOREIGN KEY REFERENCES CustomerCustomFields(Id),
    OptionValue NVARCHAR(255) NOT NULL
);

-- Δημιουργία πίνακα για την αποθήκευση των τιμών ανά πελάτη
CREATE TABLE CustomerCustomFieldValues (
    Id INT PRIMARY KEY IDENTITY,
    CustomerId INT NOT NULL,
    CustomFieldId INT NOT NULL,
    FieldValue NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    FOREIGN KEY (CustomFieldId) REFERENCES CustomerCustomFields(Id)
);

-- Δημιουργία πίνακα για την αποθήκευση ιστορικού αλλαγών των πεδίων
CREATE TABLE CustomFieldChangeHistory (
    Id INT PRIMARY KEY IDENTITY,
    CustomerId INT NOT NULL,
    CustomFieldId INT NOT NULL,
    OldValue NVARCHAR(255),
    NewValue NVARCHAR(255),
    ChangedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    FOREIGN KEY (CustomFieldId) REFERENCES CustomerCustomFields(Id)
);
