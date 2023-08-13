<!-- 
getUserInfo(){
    this.loader.showProgressBar();
    this.userService.getUserInfo()
    .pipe(finalize(()=>{this.loader.hideProgressBar();})
    .subscribe((response) => {
        localStorage.setItem('user', response);
    })
}
-->
<!-- 
COMMON IMPORT
    private loader: LoadingService,
    private notify: NotificationService,
    private userService: UserService,
    private adminService: AdminService,
    private router: Router,
    private activateRoute: ActivatedRoute,
    private userInfo: UserInfoService,
    private dialogService: OpenDialogService,
 -->
 <!-- 
if (response.code === CONFIG.STATUS_CODE.SUCCESS) {
    this.order = response.data;
}
else if (response.code === CONFIG.STATUS_CODE.ERROR) {
    this.notification.notifyError(response.message || 'Failed!');
}
 -->