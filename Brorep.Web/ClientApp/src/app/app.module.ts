import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { JudgeComponent } from './components/judge/judge.component';
import { LeaderBoardComponent } from './components/leaderboard/leaderboard.component';
import { UserComponent } from './components/user/user.component';
import { VideoComponent } from './components/video/video.component';

import { IdentityClient, SeasonClient, WorkoutClient } from './brorep-api';
import { JwtService } from './services/jwt.service';
import { UserService } from './services/user.service';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AddAuthenticationInterceptor } from './interceptors/addauthenticationheader.interceptor';
import { PreviewComponent } from './components/submit/preview/preview.component';
import { LoadVideoComponent } from './components/submit/loadvideo/loadvideo.component';
import { VideoEditComponent } from './components/submit/videoedit/videoedit.component';
import { WorkoutListComponent } from './components/submit/workoutlist/workoutlist.component';
import { UrlPrettifierPipe } from './pipes/urlprettifier.pipe';
import { SubmitComponent } from './components/submit/submit.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    JudgeComponent,
    LeaderBoardComponent,
    UserComponent,
    SubmitComponent,
    VideoComponent,
    PreviewComponent,
    LoadVideoComponent,
    VideoEditComponent,
    WorkoutListComponent,
    UrlPrettifierPipe
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule, ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'judge', component: JudgeComponent },
      { path: 'leaderboard', component: LeaderBoardComponent },
      { path: 'user', component: UserComponent },
      { path: 'submit', component: SubmitComponent },
      { path: 'workout/:seasonname/:workoutname', component: UserComponent}
    ])
  ],
  providers: [IdentityClient, JwtService, UserService, SeasonClient, WorkoutClient, {
    provide: HTTP_INTERCEPTORS,
    useClass: AddAuthenticationInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(private userService: UserService) {
    this.userService.populate(); // load current user if token exists
  }
}
