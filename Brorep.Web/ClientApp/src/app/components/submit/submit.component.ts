import { Component, OnInit } from '@angular/core';
import { RecordedRep } from 'src/app/models/recordedrep.models';
import { WorkoutDto, WorkoutClient, SubmitWorkoutCommand, RepDto } from 'src/app/brorep-api';
import { UserService } from '../../services/user.service';

@Component({
    selector: 'app-submit',
    templateUrl: './submit.component.html',
    styleUrls: ['./submit.component.scss']
})
export class SubmitComponent implements OnInit {
    step: number;
    videoUrl: string;
    workout: WorkoutDto;
    reps: RecordedRep[];

    constructor(private workoutClient: WorkoutClient, private userService: UserService) {}

    ngOnInit(): void {
        this.step = 0;
        this.reps = [];
    }

    onGoBack() {
        this.step -= 1;
        if (this.step === 0) {
            this.reps = [];
            this.videoUrl = null;
        }
    }

    onWorkoutSelected(workout) {
        this.step += 1;
        this.workout = workout;
    }

    onVideoSelected(videoUrl) {
        this.step += 1;
        this.videoUrl = videoUrl;
    }

    gotoPreview(event) {
        this.reps = event.reps;
        this.step += 1;
    }

    onSubmit(event) {
        const cmd = new SubmitWorkoutCommand(
            {
                reps: this.reps.map((x) => {
                    const rep = new RepDto();
                    rep.startTime = x.startTime;
                    rep.stopTime = x.stopTime;
                    return rep;
                }),
                videoUrl: this.videoUrl,
                workoutId: this.workout.workoutId,
                username: this.userService.getCurrentUser().user
            });

        this.workoutClient.submit(cmd).subscribe(
        result => {
            this.step += 1;
          }, error => {
            console.error(error);
          }
        );
    }
}
