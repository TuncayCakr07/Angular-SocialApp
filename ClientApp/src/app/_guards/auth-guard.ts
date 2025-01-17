import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../_services/auth.service";
import { Injectable } from "@angular/core";


@Injectable({
  providedIn:'root'
})
export class AuthGuard implements CanActivate{

  constructor(private authService:AuthService,private router:Router){}
  canActivate(){
    if(this.authService.loggedIn){
      return true;
    }
    console.log("auth guard");
    this.router.navigate(['/home']);
    return true;
  }
}
