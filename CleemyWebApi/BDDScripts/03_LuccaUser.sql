CREATE TABLE LuccaUser(
	id BIGINT IDENTITY(1,1) NOT NULL,
	firstName VARCHAR(250) NOT NULL,
	lastName VARCHAR(250) NOT NULL,
	currencyId BIGINT NOT NULL,
	CONSTRAINT PK_LuccaUser PRIMARY KEY CLUSTERED (id)
)

ALTER TABLE LuccaUser WITH CHECK ADD CONSTRAINT fk_LuccaUser_curencyId FOREIGN KEY(currencyId)
REFERENCES Currency (id)