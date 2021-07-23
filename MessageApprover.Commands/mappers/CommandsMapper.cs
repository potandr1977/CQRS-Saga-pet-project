using MessageApprover.CommandsAbstractions.Author;
using MessageApprover.Domain;

namespace MessageApprover.Commands.Mappers
{
    public static class CommandsMapper
    {
        public static Author ToDomain(this CreateAuthorCommand command)
        {
            return new Author { 
                Id = command.Id,
                Name = command.Name
            };
        }

        public static EnteredMessage ToDomain(this CreateEnteredMessageCommand command)
        {
            return new EnteredMessage
            {
                Id = command.Id,
                AuthorId = command.AuthorId,
                Text = command.Text
            };
        }

        public static EnteredMessage ToDomain(this MessageApprovedCommand command)
        {
            return new EnteredMessage
            {
                Id = command.Id,
                AuthorId = command.AuthorId,
                Text = command.Text
            };
        }
    }
}
