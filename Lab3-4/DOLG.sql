CREATE TABLE dolgn(
	id_dolg INT NOT NULL PRIMARY KEY,
    name VARCHAR(45) NOT NULL UNIQUE,
    zp DECIMAL(10,2) NOT NULL)