using Microsoft.Extensions.Logging;
using InterfacesDLL.Interfaces;
using AutoMapper;
using ModelsDLL.Models;
using ModelsDLL.DTO;

namespace ServicesDLL.Services {
    public class KotServices : IKotServices {

        private IKotRepo _kotRepo;
        private readonly ILogger<KotServices> _logger;
        private readonly IMapper _mapper;

        public KotServices(ILogger<KotServices> logger, IKotRepo repo, IMapper mapper) {
            _logger = logger;
            _kotRepo = repo;
            _mapper = mapper;
        }

        public List<Kot> GetAll() {
            _logger.LogDebug("entering GetAll() in KotServices");
            List<KotStudentDTO> kotsDTO = _kotRepo.GetAll();
            List<Kot> kots = _mapper.Map<List<Kot>>(kotsDTO);
            _logger.LogDebug("Fetched {Count} kots from database (service).", kotsDTO.Count);
            return kots;
        }

        public void Delete(int id) {
            _kotRepo.Delete(id);
        }

    }
}
