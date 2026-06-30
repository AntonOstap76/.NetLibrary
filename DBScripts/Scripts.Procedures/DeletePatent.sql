-- PROCEDURE: public.DeletePatent(uuid)

-- DROP PROCEDURE IF EXISTS public."DeletePatent"(uuid);

CREATE OR REPLACE PROCEDURE public."DeletePatent"(
	IN "Id" uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	DELETE FROM "Patent" WHERE "Id"="Id";
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to delete Patent';
END
$BODY$;
ALTER PROCEDURE public."DeletePatent"(uuid)
    OWNER TO postgres;

