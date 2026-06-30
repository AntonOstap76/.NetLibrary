-- PROCEDURE: public.CreateMagazineIssue(uuid, smallint, uuid, date, text)

-- DROP PROCEDURE IF EXISTS public."CreateMagazineIssue"(uuid, smallint, uuid, date, text);

CREATE OR REPLACE PROCEDURE public."CreateMagazineIssue"(
	IN "Id" uuid,
	IN "Number" smallint,
	IN "MagazineId" uuid,
	IN "PublishDate" date,
	IN "Content" text)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	INSERT INTO "MagazineIssue"("Id", "Number", "MagazineId",
	"PublishDate", "Content")
	VALUES("Id", "Number", "MagazineId",
	"PublishDate", "Content");
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to create MagazineIssue';
END
$BODY$;
ALTER PROCEDURE public."CreateMagazineIssue"(uuid, smallint, uuid, date, text)
    OWNER TO postgres;

