-- PROCEDURE: public.CreateCountry(character varying, character varying, character varying)

-- DROP PROCEDURE IF EXISTS public."CreateCountry"(character varying, character varying, character varying);

CREATE OR REPLACE PROCEDURE public."CreateCountry"(
	IN "Code" character varying,
	IN "Name" character varying,
	IN "Language" character varying)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	INSERT INTO "Country"("Code",
	"Name",
	"Language")
	VALUES("Code",
	"Name",
	"Language");
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to create Country';

END
$BODY$;
ALTER PROCEDURE public."CreateCountry"(character varying, character varying, character varying)
    OWNER TO postgres;

