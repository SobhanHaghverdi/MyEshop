namespace MyEshop.Components
{
    public class ProductGroupsComponent : ViewComponent
    {
        private IGroupRepository _groupRepository;
        public ProductGroupsComponent(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View("/Views/Components/ProductGroupsComponent.cshtml", _groupRepository.GetGroupForShow()));
        }
    }
}
