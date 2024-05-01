-- Create the 'Posts' table
--post_text is VarChar due to size limit, should adjust based on requirements
CREATE TABLE Posts (
    posts_id INT NOT NULL,
    post_text VARCHAR(5000) NOT NULL,
    users_id INT,
    CONSTRAINT PK_Posts PRIMARY KEY (posts_id),
    CONSTRAINT FK_Posts_Users FOREIGN KEY (users_id) REFERENCES Users(users_id)
);

-- Create the 'Families' table
CREATE TABLE Families (
    families_id INT NOT NULL,
    family_name NVARCHAR(255) NOT NULL,
    CONSTRAINT PK_Families PRIMARY KEY (families_id)
);

-- Create the 'Civs' table
CREATE TABLE Civs (
    civs_id INT NOT NULL,
    civ_name NVARCHAR(255) NOT NULL,
    CONSTRAINT PK_Civs PRIMARY KEY (civs_id)
);

-- Create the 'Replies' table
CREATE TABLE Replies (
    replies_id INT NOT NULL,
    reply_text NVARCHAR(1000) NOT NULL,
    posts_id INT,
    users_id INT,
    CONSTRAINT PK_Replies PRIMARY KEY (replies_id),
    CONSTRAINT FK_Replies_Posts FOREIGN KEY (posts_id) REFERENCES Posts(posts_id),
    CONSTRAINT FK_Replies_Users FOREIGN KEY (users_id) REFERENCES Users(users_id)
);

-- Create the 'Pictures' table
CREATE TABLE Pictures (
    pictures_id INT NOT NULL,
    CONSTRAINT PK_Pictures PRIMARY KEY (pictures_id)
);
