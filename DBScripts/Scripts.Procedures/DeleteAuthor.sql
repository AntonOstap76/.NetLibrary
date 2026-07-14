-- PROCEDURE: public.DeleteAuthor(uuid)

-- DROP PROCEDURE IF EXISTS public."DeleteAuthor"(uuid);

CREATE OR REPLACE PROCEDURE public."DeleteAuthor"(
	IN "Id" uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	DELETE FROM "Author" WHERE "Id" = "Id";
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to delete Author';

END;
$BODY$;
ALTER PROCEDURE public."DeleteAuthor"(uuid)
    OWNER TO postgres;

