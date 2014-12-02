module.exports = function (grunt) {

    grunt.initConfig({
        uglify: {
            options: {
                //banner: '/*! <%= grunt.template.today("yyyy-mm-dd") %> CollectionHUB&#8482; ALL RIGHTS RESERVED */\n'
                },
            my_target: {
                files: {
                    'js/bootstrap.min.js':      ['js/bootstrap.js'],
                    'js/DebtView.min.js':       ['js/DebtView.js'],
                    'js/DebtSearch.min.js':     ['js/DebtSearch.js'],
                    'js/moment.min.js':         ['js/moment.js'],
                    'js/NewProcess.min.js':     ['js/NewProcess.js'],
                    'js/Processes.min.js':      ['js/Processes.js'],
                    'js/ProcessResults.min.js': ['js/ProcessResults.js'],
                    'js/ProcessView.min.js':    ['js/ProcessView.js'],
                    'js/sb-admin.min.js':       ['js/sb-admin.js'],
                    'js/Search.min.js':         ['js/Search.js']
                }
            }
        },
        cssmin: {
            options: {
                keepSpecialComments: 0
                //banner: '/*! <%= grunt.template.today("yyyy-mm-dd") %> CollectionHUB&#8482; ALL RIGHTS RESERVED */\n'
            },
            my_target: {
                files: {
                      'css/sb-admin.min.css':       ['css/sb-admin.css'],
                      'css/bootstrap.min.css':      ['css/bootstrap.css'],
                      'css/bootstrap-ie7.min.css':  ['css/bootstrap-ie7.css']
                }
            }
        },
        htmlmin: {
            options: {
                collapseWhitespace: true,
                minifyJS: true
                //keepSpecialComments: 0
                //banner: '/*! <%= grunt.template.today("yyyy-mm-dd") %> CollectionHUB&#8482; ALL RIGHTS RESERVED */\n'
            },
            my_target: {
                files: {
                    'modals/ApproveBatch.min.html'              : 'modals/ApproveBatch.html',
                    'modals/CancelBatch.min.html'               : 'modals/CancelBatch.html',
                    'modals/ConfirmCreateRecoveryCycle.min.html': 'modals/ConfirmCreateRecoveryCycle.html',
                    'modals/CreateArrangement.min.html'         : 'modals/CreateArrangement.html',
                    'modals/CreateDebtAction.min.html'          : 'modals/CreateDebtAction.html',
                    'modals/CreateDebtActionDocument.min.html'  : 'modals/CreateDebtActionDocument.html',
                    'modals/CreateDebtAttributes.min.html'      : 'modals/CreateDebtAttributes.min.html',
                    'modals/CreateDebtGroup.min.html'           : 'modals/CreateDebtGroup.html',
                    'modals/CreateDebtNote.min.html'            : 'modals/CreateDebtNote.html',
                    'modals/CreateNewTemplate.min.html'         : 'modals/CreateNewTemplate.html',
                    'modals/CreatePersonAttributes.min.html'    : 'modals/CreatePersonAttributes.html',
                    'modals/CreateRecoveryCycle.min.html'       : 'modals/CreateRecoveryCycle.html',
                    'modals/Index.min.html'                     : 'modals/Index.html',
                    'modals/RemoveDebtGroup.min.html'           : 'modals/RemoveDebtGroup.html',
                }
            }
        }
    });

    // Load the plugin that provides the "uglify" task.
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-cssmin');
    grunt.loadNpmTasks('grunt-contrib-htmlmin');

    // Default task(s).
    grunt.registerTask('default', ['uglify', 'cssmin', 'htmlmin']);
    grunt.registerTask('gruntCss', ['cssmin']);
};