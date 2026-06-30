-- PROCEDURE: public.UpdateMagazine(uuid, character varying, character varying, uuid, date, date)

-- DROP PROCEDURE IF EXISTS public."UpdateMagazine"(uuid, character varying, character varying, uuid, date, date);

CREATE OR REPLACE PROCEDURE public."UpdateMagazine"(
	IN "Id" uuid,
	IN "Issn" character varying,
	IN "Title" character varying,
	IN "PublisherId" uuid,
	IN "PublishDate" date,
	IN "EndOfPublish" date)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	UPDATE "Magazine"
    SET
        "Issn"    = "Issn",
        "Title"   = "Title",
        "PublisherId" = "PublisherId",
		"PublishDate" = "PublishDate",
		"EndOfPublish" = "EndOfPublish"
    WHERE "Id" = "Id";

	COMMIT;

	
	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to update Magazine';
END
$BODY$;
ALTER PROCEDURE public."UpdateMagazine"(uuid, character varying, character varying, uuid, date, date)
    OWNER TO postgres;

