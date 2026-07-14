-- PROCEDURE: public.DeleteBook(uuid)

-- DROP PROCEDURE IF EXISTS public."DeleteBook"(uuid);

CREATE OR REPLACE PROCEDURE public."DeleteBook"(
	IN "Book" uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	DELETE FROM "Book" WHERE "Id"= "Id";
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to delete Book';
END
$BODY$;
ALTER PROCEDURE public."DeleteBook"(uuid)
    OWNER TO postgres;

