import gulp  from 'gulp';
import run from 'run-sequence';
import unity from 'gulp-unity';

/// Dev target pattern
var dev = 'Network';

gulp.task('default', function(callback) {
  run('tests', callback);
});

// Run unity tests
gulp.task('tests', function() {
  return gulp.src('./package.json')
    .pipe(unity({
      method: 'N.TestRunner.Run',
      args: ['--filterTests=N.'],
      debug: (v) => {
        v.debug([
          { pattern: /^\! Test.*/, color: 'red' },
          { pattern: /^\- Test.*/, color: 'green' },
          { pattern: /test_.*/, color: 'red', context: true },
          { pattern: /.*DEBUG.*/, color: 'yellow', context: 0 },
          { pattern: /.*ERROR.*/, color: 'red', context: 0 },
        ])
      }
    }));
});

// Run unity tests
gulp.task('dev', function() {
  return gulp.src('./package.json')
    .pipe(unity({
      method: 'N.TestRunner.Run',
      args: [`--filterTests=${dev}`],
      debug: (v) => {
        v.debug([
          { pattern: /^\! Test.*/, color: 'red' },
          { pattern: /^\- Test.*/, color: 'green' },
          { pattern: /test_.*/, color: 'red', context: true },
          { pattern: /.*DEBUG.*/, color: 'yellow', context: 0 },
          { pattern: /.*ERROR.*/, color: 'red', context: 0 },
        ]);
      }
    }));
});
