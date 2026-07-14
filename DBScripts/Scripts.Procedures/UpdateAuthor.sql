-- PROCEDURE: public.UpdateAuthor(uuid, character varying, character varying, smallint, character varying)

-- DROP PROCEDURE IF EXISTS public."UpdateAuthor"(uuid, character varying, character varying, smallint, character varying);

CREATE OR REPLACE PROCEDURE public."UpdateAuthor"(
	IN "Id" uuid,
	IN "Name" character varying,
	IN "LastName" character varying,
	IN "BirthYear" smallint,
	IN "CountryId" character varying)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	UPDATE "Author"
    SET
        "Name"    = "Name",
        "LastName"   = "LastName",
        "BirthYear" = "BirthYear",
		"CountryId" = "CountryId"
    WHERE "Id" = "Id";

	COMMIT;
	
	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to update Author';
END
$BODY$;
ALTER PROCEDURE public."UpdateAuthor"(uuid, character varying, character varying, smallint, character varying)
    OWNER TO postgres;

