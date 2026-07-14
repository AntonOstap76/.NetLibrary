-- PROCEDURE: public.CreateAuthor(uuid, character varying, character varying, smallint, character varying)

-- DROP PROCEDURE IF EXISTS public."CreateAuthor"(uuid, character varying, character varying, smallint, character varying);

CREATE OR REPLACE PROCEDURE public."CreateAuthor"(
	IN "Id" uuid,
	IN "Name" character varying,
	IN "LastName" character varying,
	IN "BirthYear" smallint,
	IN "CountryId" character varying)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	INSERT INTO "Author"("Id",
	"Name",
	"LastName",
	"BirthYear",
	"CountryId")
	VALUES("Id",
	"Name",
	"LastName",
	"BirthYear",
	"CountryId");
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to create Author';
END
$BODY$;
ALTER PROCEDURE public."CreateAuthor"(uuid, character varying, character varying, smallint, character varying)
    OWNER TO postgres;

