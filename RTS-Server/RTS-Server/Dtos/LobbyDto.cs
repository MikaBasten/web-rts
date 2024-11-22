namespace RTS_Server.Dtos
{
    public class LobbyDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int PlayerLimit { get; set; }
        public List<PlayerDto>? Players { get; set; }
    }
}
