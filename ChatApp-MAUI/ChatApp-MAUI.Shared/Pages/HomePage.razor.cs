using Microsoft.AspNetCore.Components;
using ChatApp_MAUI.Domain.Entities;
using ChatApp_MAUI.Infrastructure.Services.FriendServices;
using ChatApp_MAUI.Infrastructure.Services.CustomAuthenticationServices;
using ChatApp_MAUI.Infrastructure.CallBackServices.LoadFriendsCallback;
using ChatApp_MAUI.Infrastructure.Services.MessageServices;
using ChatApp_MAUI.Infrastructure.Services;
using ChatApp_MAUI.Infrastructure;
using ChatApp_MAUI.Infrastructure.Services.CallBackServices.ConversationsCallback;
using Microsoft.JSInterop;

namespace ChatApp_MAUI.Shared.Pages
{
    public partial class HomePageBase : ComponentBase, ICallBackService, IConversationCallback
    {
        [Inject] protected IFriendService _friendService { get; set; } = default!;
        [Inject] protected ILoginService _loginService { get; set; } = default!;
        [Inject] protected ICallBackService _callBackService { get; set; } = default!;
        [Inject] protected IConversationCallback _conversationCallback { get; set; } = default!;
        [Inject] protected IMessageService _messageService { get; set; } = default!;
        [Inject] protected IJSRuntime _jsRuntime { get; set; } = default!;
        [Inject] protected LayoutNotifierService _notifierService { get; set; } = default!;
        [Inject] protected AppStateService _appStateService { get; set; } = default!;
        protected List<FriendsModel> friends = new();
        protected int loaderCount = 20;
        protected bool showConversation = true;
        protected int WindowWidth;
        protected override async Task OnInitializedAsync()
        {
            WindowWidth = await _jsRuntime.InvokeAsync<int>("eval", "window.innerWidth");
            if(WindowWidth <= 600)
            {
                showConversation = false;
            }
            _callBackService.RegisterCallback(this);
            _notifierService.OnChanged += HandleChange;
            if(_appStateService.User.Uid != null)
            {
                friends = await _friendService.GetFriends(_appStateService.User.Uid, _appStateService.Token);
            }
            StateHasChanged();
            if (_conversationCallback is ConversationCallback callback)
            {
                callback.RegisterCallBack(this);
            }
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await _jsRuntime.InvokeVoidAsync("onResizeGetWidth", DotNetObjectReference.Create(this));
                WindowWidth = await _jsRuntime.InvokeAsync<int>("eval", "window.innerWidth");
                StateHasChanged();
            }
        }
        [JSInvokable]
        public void OnResizeWidthCaptured(int width)
        {
            if (WindowWidth == width)
                return;
            WindowWidth = width;
            if(WindowWidth <= 600)
            {
                showConversation = false;
            } else {
                showConversation = true;
            }
            StateHasChanged();
            //Console.WriteLine(showConversation);
        }
        private void HandleChange()
        {
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            _notifierService.OnChanged -= HandleChange;
        }
        public async Task OnShowFrieds()
        {
            friends = await _friendService.GetFriends(_appStateService.User.Uid, _appStateService.Token);
            StateHasChanged();
        }

        public void RegisterCallback(ICallBackService listener)
        {
            _callBackService = listener;
        }
        public void RegisterCallBack(IConversationCallback callback)
        {
            _conversationCallback = callback;
        }
        public Task OnShowConversation(AuthTokenModel user)
        {
            showConversation = true;
            StateHasChanged();
            return Task.CompletedTask;
        }
        protected void CloseConversation()
        {
            showConversation = false;
            StateHasChanged();
        }
        protected void MessageSent()
        {
            showConversation = true;
            StateHasChanged();
        }
    }
}
