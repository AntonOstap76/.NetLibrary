-- PROCEDURE: public.UpdatePublisher(uuid, character varying, character varying, smallint)

-- DROP PROCEDURE IF EXISTS public."UpdatePublisher"(uuid, character varying, character varying, smallint);

CREATE OR REPLACE PROCEDURE public."UpdatePublisher"(
	IN "Id" uuid,
	IN "Name" character varying,
	IN "CountryId" character varying,
	IN "FoundedYear" smallint)
LANGUAGE 'plpgsql'
AS $BODY$
BEGIN 
	UPDATE "Publisher"
    SET
        "Name"    = "Name",
        "CountryId"   = "CountryId",
        "FoundedYear" = "FoundedYear"
    WHERE "Id" = "Id";

	COMMIT;

	
	EXCEPTION
    WHEN OTHERS THEN
        RAISE EXCEPTION 'Failed to update Publisher';
END
$BODY$;
ALTER PROCEDURE public."UpdatePublisher"(uuid, character varying, character varying, smallint)
    OWNER TO postgres;

