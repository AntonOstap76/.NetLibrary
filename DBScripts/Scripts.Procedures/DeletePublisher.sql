-- PROCEDURE: public.DeletePublisher(uuid)

-- DROP PROCEDURE IF EXISTS public."DeletePublisher"(uuid);

CREATE OR REPLACE PROCEDURE public."DeletePublisher"(
	IN "Id" uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	DELETE FROM "Publisher" WHERE "Id" = "Id";
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to delete Publisher';

END
$BODY$;
ALTER PROCEDURE public."DeletePublisher"(uuid)
    OWNER TO postgres;

