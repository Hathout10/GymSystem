using AutoMapper;
using GymSystem.BLL.Dtos.MonthlyPlan;
using GymSystem.BLL.Interfaces;
using GymSystem.BLL.Specifications.MonthlyPlan_Specification;
using GymSystem.BLL.Specifications;
using GymSystem.DAL.Entities;

public class MonthlyPlanService : IMonthlyPlanService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MonthlyPlanService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MonthlyPlanDto> GetMonthlyPlanByIdAsync(int id)
    {
        var monthlyPlan = await _unitOfWork.Repository<MonthlyPlan>().GetByIdAsync(id);
        return _mapper.Map<MonthlyPlanDto>(monthlyPlan);
    }

    public async Task<IReadOnlyList<MonthlyPlanDto>> GetAllMonthlyPlansWithSpecAsync(SpecPrams specParams)
    {
        var monthlyPlans = await _unitOfWork.Repository<MonthlyPlan>().GetAllWithSpecAsync(new MonthlyPlanSpecification(specParams));
        return _mapper.Map<IReadOnlyList<MonthlyPlanDto>>(monthlyPlans);
    }

    public async Task<MonthlyPlanDto> CreateMonthlyPlanAsync(CreateMonthlyPlanDto createMonthlyPlanDto)
    {
        var monthlyPlan = _mapper.Map<MonthlyPlan>(createMonthlyPlanDto);
        await _unitOfWork.Repository<MonthlyPlan>().Add(monthlyPlan);
        await _unitOfWork.Complete();
        return _mapper.Map<MonthlyPlanDto>(monthlyPlan);
    }

    public async Task<MonthlyPlanDto> UpdateMonthlyPlanAsync(int id, UpdateMonthlyPlanDto updateMonthlyPlanDto)
    {
        var existingPlan = await _unitOfWork.Repository<MonthlyPlan>().GetByIdAsync(id);
        if (existingPlan == null) return null;

        _mapper.Map(updateMonthlyPlanDto, existingPlan);
        _unitOfWork.Repository<MonthlyPlan>().Update(existingPlan);
        await _unitOfWork.Complete();
        return _mapper.Map<MonthlyPlanDto>(existingPlan);
    }

    public async Task<bool> DeleteMonthlyPlanAsync(int id)
    {
        var monthlyPlan = await _unitOfWork.Repository<MonthlyPlan>().GetByIdAsync(id);
        if (monthlyPlan == null) return false;

        _unitOfWork.Repository<MonthlyPlan>().Delete(monthlyPlan);
        await _unitOfWork.Complete();
        return true;
    }

    public async Task<MonthlyPlanDto> StopMonthlyPlanAsync(int id)
    {
        var monthlyPlan = await _unitOfWork.Repository<MonthlyPlan>().GetByIdAsync(id);
        if (monthlyPlan == null) return null;

        monthlyPlan.IsStopped = true;
        monthlyPlan.StopDate = DateTime.Now;
        _unitOfWork.Repository<MonthlyPlan>().Update(monthlyPlan);
        await _unitOfWork.Complete();
        return _mapper.Map<MonthlyPlanDto>(monthlyPlan);
    }

    public async Task<int> CountAsync(SpecPrams specParams)
    {
        return await _unitOfWork.Repository<MonthlyPlan>().GetCountAsync(new MonthlyPlanForCountingSpecification(specParams));
    }
}