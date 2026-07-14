-- PROCEDURE: public.DeleteMagazineIssue(uuid)

-- DROP PROCEDURE IF EXISTS public."DeleteMagazineIssue"(uuid);

CREATE OR REPLACE PROCEDURE public."DeleteMagazineIssue"(
	IN "Id" uuid)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN
	DELETE FROM "MagazineIssue" WHERE "Id" = "Id";
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to delete MagazineIssue';

END
$BODY$;
ALTER PROCEDURE public."DeleteMagazineIssue"(uuid)
    OWNER TO postgres;

