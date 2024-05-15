using System.Collections.Concurrent;
using UtilityBot.Services.Interfaces;
using VoiceTexterBot.Models;

namespace UtilityBot.Services.Implementations
{
    public class MemoryStorage : IStorage
    {
        /// <summary>
        /// Хранилище сессий
        /// </summary>
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            var newSession = new Session() { ActionCode = "count" };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}