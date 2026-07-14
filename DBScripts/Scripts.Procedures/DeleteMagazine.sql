-- PROCEDURE: public.DeleteMagazine(uuid)

-- DROP PROCEDURE IF EXISTS public."DeleteMagazine"(uuid);

CREATE OR REPLACE PROCEDURE public."DeleteMagazine"(
	IN "Id" uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	DELETE FROM "Magazine" WHERE "Id" = "Id";
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to delete Magazine';

END
$BODY$;
ALTER PROCEDURE public."DeleteMagazine"(uuid)
    OWNER TO postgres;

