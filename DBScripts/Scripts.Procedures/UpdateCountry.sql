-- PROCEDURE: public.UpdateCountry(character varying, character varying, character varying)

-- DROP PROCEDURE IF EXISTS public."UpdateCountry"(character varying, character varying, character varying);

CREATE OR REPLACE PROCEDURE public."UpdateCountry"(
	IN "Code" character varying,
	IN "Name" character varying,
	IN "Language" character varying)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	UPDATE "Country"
    SET
        "Code"    = "Code",
        "Name"   = "Name",
        "Language" = "Language"
    WHERE "Id" = "Id";

	COMMIT;
	
	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to update Country';
END
$BODY$;
ALTER PROCEDURE public."UpdateCountry"(character varying, character varying, character varying)
    OWNER TO postgres;

