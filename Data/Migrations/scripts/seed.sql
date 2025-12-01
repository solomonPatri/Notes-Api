-- =============================================================
-- 👤 USERS
-- =============================================================
INSERT INTO users (email, password, age) VALUES
('user1@example.com', 'pass1', 21),
('user2@example.com', 'pass2', 22),
('user3@example.com', 'pass3', 23),
('user4@example.com', 'pass4', 24),
('user5@example.com', 'pass5', 25),
('user6@example.com', 'pass6', 26),
('user7@example.com', 'pass7', 27),
('user8@example.com', 'pass8', 28),
('user9@example.com', 'pass9', 29),
('user10@example.com', 'pass10', 30);

-- =============================================================
-- 📝 NOTES (4 per user)
-- Titles: Note A, B, C, D
-- =============================================================
INSERT INTO notes (userId, title, content, isArchived, createdAt, updatedAt)
SELECT id, 'Note A', 'Content A', 0, UTC_TIMESTAMP(), NULL FROM users;
INSERT INTO notes (userId, title, content, isArchived, createdAt, updatedAt)
SELECT id, 'Note B', 'Content B', 0, UTC_TIMESTAMP(), NULL FROM users;
INSERT INTO notes (userId, title, content, isArchived, createdAt, updatedAt)
SELECT id, 'Note C', 'Content C', 0, UTC_TIMESTAMP(), NULL FROM users;
INSERT INTO notes (userId, title, content, isArchived, createdAt, updatedAt)
SELECT id, 'Note D', 'Content D', 0, UTC_TIMESTAMP(), NULL FROM users;

-- =============================================================
-- 🧩 NOTE CATEGORIES (2 per note)
-- Pattern by title:
-- A → Personal (0), Todo (3)
-- B → Study (2), Important (4)
-- C → Work (1), Todo (3)
-- D → Personal (0), Important (4)
-- =============================================================
-- Note A
INSERT INTO noteCategories (noteId, category)
SELECT n.id, 0 FROM notes n WHERE n.title='Note A'; -- Personal
INSERT INTO noteCategories (noteId, category)
SELECT n.id, 3 FROM notes n WHERE n.title='Note A'; -- Todo

-- Note B
INSERT INTO noteCategories (noteId, category)
SELECT n.id, 2 FROM notes n WHERE n.title='Note B'; -- Study
INSERT INTO noteCategories (noteId, category)
SELECT n.id, 4 FROM notes n WHERE n.title='Note B'; -- Important

-- Note C
INSERT INTO noteCategories (noteId, category)
SELECT n.id, 1 FROM notes n WHERE n.title='Note C'; -- Work
INSERT INTO noteCategories (noteId, category)
SELECT n.id, 3 FROM notes n WHERE n.title='Note C'; -- Todo

-- Note D
INSERT INTO noteCategories (noteId, category)
SELECT n.id, 0 FROM notes n WHERE n.title='Note D'; -- Personal
INSERT INTO noteCategories (noteId, category)
SELECT n.id, 4 FROM notes n WHERE n.title='Note D'; -- Important
