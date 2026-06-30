-- PROCEDURE: public.CreatePublisher(uuid, character varying, character varying, smallint)

-- DROP PROCEDURE IF EXISTS public."CreatePublisher"(uuid, character varying, character varying, smallint);

CREATE OR REPLACE PROCEDURE public."CreatePublisher"(
	IN "Id" uuid,
	IN "Name" character varying,
	IN "CountryId" character varying,
	IN "FoundedYear" smallint)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	INSERT INTO "Publisher"("Id", "Name", "CountryId", "FoundedYear")
	VALUES("Id", "Name", "CountryId", "FoundedYear");
	COMMIT;

	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to create Publisher';
END
$BODY$;
ALTER PROCEDURE public."CreatePublisher"(uuid, character varying, character varying, smallint)
    OWNER TO postgres;

