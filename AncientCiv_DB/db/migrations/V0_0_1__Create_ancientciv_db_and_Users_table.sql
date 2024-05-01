
-- Create the 'users' table
CREATE TABLE Users (
    users_id INT NOT NULL,
    persona_name NVARCHAR(255) NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY (users_id)
);
