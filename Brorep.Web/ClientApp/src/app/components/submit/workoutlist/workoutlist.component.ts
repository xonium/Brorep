import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { SeasonClient, SeasonWorkoutsDto } from 'src/app/brorep-api';

@Component({
    selector: 'app-workoutlist',
    templateUrl: './workoutlist.component.html',
    styleUrls: ['./workoutlist.component.scss']
})
export class WorkoutListComponent implements OnInit {
    constructor(private seasonClient: SeasonClient) { }

    @Output() workoutSelected = new EventEmitter();

    public isLoading: boolean;
    public seasonWorkouts: SeasonWorkoutsDto;

    ngOnInit(): void {
        this.isLoading = true;
        this.seasonClient.getWorkoutsForSeason().subscribe(
            data => {
                this.isLoading = false;
                this.seasonWorkouts = data;
            },
            error => {}
        );
    }

    onWorkoutClick(workoutId) {
        this.workoutSelected.emit(workoutId);
    }
}
