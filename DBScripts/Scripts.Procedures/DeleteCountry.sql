-- PROCEDURE: public.DeleteCountry(character varying)

-- DROP PROCEDURE IF EXISTS public."DeleteCountry"(character varying);

CREATE OR REPLACE PROCEDURE public."DeleteCountry"(
	IN "Code" character varying)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	DELETE FROM "Country" WHERE "Id"="Id";
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to delete Country';
END
	
$BODY$;
ALTER PROCEDURE public."DeleteCountry"(character varying)
    OWNER TO postgres;

