USE ancientciv;
-- Insert test data into the 'Users' table
INSERT INTO Users (users_id, persona_name)
VALUES
    (1, 'User1'),
    (2, 'User2'),
    (3, 'User3');

-- Insert test data into the 'Posts' table
INSERT INTO Posts (posts_id, post_text, users_id)
VALUES
    (1, 'Test Post 1', 1),
    (2, 'Test Post 2', 2),
    (3, 'Test Post 3', 1);

-- Insert test data into the 'Families' table
INSERT INTO Families (families_id, family_name)
VALUES
    (1, 'Family1'),
    (2, 'Family2'),
    (3, 'Family3');

-- Insert test data into the 'Civs' table
INSERT INTO Civs (civs_id, civ_name)
VALUES
    (1, 'Civilization 1'),
    (2, 'Civilization 2'),
    (3, 'Civilization 3');

-- Insert test data into the 'Replies' table
INSERT INTO Replies (replies_id, reply_text, posts_id, users_id)
VALUES
    (1, 'Reply 1 to post 1 from user 2', 1, 2),
    (2, 'Reply 2 to post 2 from user 1', 2, 1),
    (3, 'Reply 3 to post 1 from user 3', 1, 3);
