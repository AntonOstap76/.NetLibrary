-- PROCEDURE: public.CreateMagazine(uuid, character varying, character varying, uuid, date, date)

-- DROP PROCEDURE IF EXISTS public."CreateMagazine"(uuid, character varying, character varying, uuid, date, date);

CREATE OR REPLACE PROCEDURE public."CreateMagazine"(
	IN "Id" uuid,
	IN "Issn" character varying,
	IN "Title" character varying,
	IN "PublisherId" uuid,
	IN "PublisherDate" date,
	IN "EndOfPublish" date)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	INSERT INTO "Magazine"("Id", "Issn", "Title", "Title", "PublisherId",
	"PublishDate", "EndOfPublish")
	VALUES("Id", "Issn", "Title", "Title", "PublisherId",
	"PublishDate", "EndOfPublish");
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to create Magazine';

END
$BODY$;
ALTER PROCEDURE public."CreateMagazine"(uuid, character varying, character varying, uuid, date, date)
    OWNER TO postgres;

