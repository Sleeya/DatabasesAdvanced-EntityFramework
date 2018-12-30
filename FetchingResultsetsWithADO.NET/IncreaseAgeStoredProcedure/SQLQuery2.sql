CREATE PROC usp_GetOlder(@MinionId INT)
AS
BEGIN
	UPDATE Minions
	SET Age += 1
	WHERE Id = @MinionId
END

