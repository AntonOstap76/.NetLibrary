-- PROCEDURE: public.UpdateMagazineIssue(uuid, smallint, uuid, date, text)

-- DROP PROCEDURE IF EXISTS public."UpdateMagazineIssue"(uuid, smallint, uuid, date, text);

CREATE OR REPLACE PROCEDURE public."UpdateMagazineIssue"(
	IN "Id" uuid,
	IN "Number" smallint,
	IN "MagazineId" uuid,
	IN "PublishDate" date,
	IN "Content" text)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	UPDATE "MagazineIssue"
    SET
        "Number"    = "Number",
        "MagazineId"   = "MagazineId",
        "PublshDate" = "PublshDate",
		"Content" = "Content"
    WHERE "Id" = "Id";

	COMMIT;
	
	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to update MagazineIssue';
END
$BODY$;
ALTER PROCEDURE public."UpdateMagazineIssue"(uuid, smallint, uuid, date, text)
    OWNER TO postgres;

