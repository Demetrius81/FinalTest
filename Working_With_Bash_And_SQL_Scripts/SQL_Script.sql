
create database friends_of_man;

use friends_of_man;

--pets

CREATE TABLE pets (
  id INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
  name VARCHAR(50) NOT NULL,
  kind_of_animal VARCHAR(50) NOT NULL,
  birth_date DATE NOT NULL,
  is_pack BOOL NOT NULL,
  commands VARCHAR(100) NOT NULL
);

INSERT INTO pets (name, kind_of_animal, birth_date, is_pack, commands) VALUES 
('Rax', 'dog', '2022-12-20', FALSE, 'Wwof, Brrr'),
('Max', 'dog', '2015-11-20', FALSE, 'Wwof, Brrr'),
('Sangwich', 'dog', '2021-05-20', FALSE, 'Wwof, Brrr'),
('Fred', 'dog', '2014-12-20', FALSE, 'Wwof, Brrr'),
('Pussy', 'cat', '2022-12-20', FALSE, 'Miau, Frrr'),
('cutlet', 'cat', '2021-10-20', FALSE, 'Miau, Frrr'),
('Maddy', 'cat', '2015-12-20', FALSE, 'Miau, Frrr'),
('Felix', 'cat', '2014-12-20', FALSE, 'Miau, Frrr'),
('Frank', 'humster', '2022-12-20', FALSE, 'Pie'),
('Pie', 'humster', '2021-11-20', FALSE, 'Pie'),
('Madison', 'humster', '2015-12-20', FALSE, 'Pie'),
('Alf', 'humster', '2014-12-20', FALSE, 'Pie');

--pack_animals

CREATE TABLE pack_animals (
  id INTEGER PRIMARY KEY AUTO_INCREMENT NOT NULL,
  name VARCHAR(50) NOT NULL,
  kind_of_animal VARCHAR(50) NOT NULL,
  birth_date DATE NOT NULL,
  is_pack BOOL NOT NULL,
  commands VARCHAR(100) NOT NULL
);

INSERT INTO pack_animals (name, kind_of_animal, birth_date, is_pack, commands) VALUES 
('Apple', 'horse', '2022-12-20', TRUE, 'Frrr'),
('Wind', 'horse', '2015-11-20', TRUE, 'Chrr, Yeho'),
('Black', 'horse', '2021-05-20', TRUE, 'Frrr'),
('Rudy', 'horse', '2014-12-20', TRUE, 'Yeho'),
('Elvis', 'camel', '2022-12-20', TRUE, 'Hrrrr.... Pah!'),
('Mike', 'camel', '2021-10-20', TRUE, 'Hrrrr.... Pah!'),
('Rihter', 'camel', '2015-12-20', TRUE, 'Hrrrr.... Pah!'),
('Memphis', 'camel', '2014-12-20', TRUE, 'Hrrrr.... Pah!'),
('Vasily', 'donkey', '2022-12-20', TRUE, 'Hee-haw'),
('Ia', 'donkey', '2021-11-20', TRUE, 'Hee-haw'),
('Dude', 'donkey', '2015-12-20', TRUE, 'Hee-haw'),
('Lightning', 'donkey', '2014-12-20', TRUE, 'Hee-haw');

DELETE FROM pack_animals WHERE kind_of_animal = 'camel';

CREATE TABLE young_animals 
SELECT *, TIMESTAMPDIFF(MONTH, birth_date, NOW()) AS age_in_month 
FROM pack_animals 
WHERE TIMESTAMPDIFF(YEAR, birth_date, NOW()) BETWEEN 1 AND 3;

INSERT INTO young_animals
SELECT *, TIMESTAMPDIFF(MONTH, birth_date, NOW()) AS age_in_month 
FROM pets 
WHERE TIMESTAMPDIFF(YEAR, birth_date, NOW()) BETWEEN 1 AND 3;

CREATE TABLE animals 
SELECT *, ('pack_animals') AS old_table FROM pack_animals;

INSERT INTO animals 
SELECT *, ('pets') AS old_table FROM pets;